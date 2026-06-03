using Dota_2_Training_Platform.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Api
{
    public static class TwitchApiClient
    {
        private const string TokenUrl = "https://id.twitch.tv/oauth2/token";
        private const string HelixBaseUrl = "https://api.twitch.tv/helix/";
        private const string Dota2GameName = "Dota 2";

        private static readonly HttpClient Http = new HttpClient();
        private static string _accessToken;
        private static DateTime _tokenExpiresAtUtc = DateTime.MinValue;
        private static string _dota2GameId;

        public static async Task<IReadOnlyList<TwitchStreamInfo>> GetTopDota2StreamsAsync(int count = 5)
        {
            if (count < 1)
                count = 1;
            if (count > 100)
                count = 100;

            if (!await EnsureAccessTokenAsync())
                throw new InvalidOperationException(GetCredentialsErrorMessage());

            string gameId = await GetDota2GameIdAsync();
            if (string.IsNullOrWhiteSpace(gameId))
                throw new InvalidOperationException("Категория Dota 2 не найдена в Twitch API.");

            string streamsUrl =
                $"{HelixBaseUrl}streams?game_id={Uri.EscapeDataString(gameId)}&first={count}";

            using (var request = CreateHelixRequest(HttpMethod.Get, streamsUrl))
            using (var response = await Http.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($"Twitch API (streams): {(int)response.StatusCode} {body}");

                var streams = ParseStreams(body);
                if (streams.Count == 0)
                    return streams;

                var users = await GetUsersByIdsAsync(streams.Select(s => s.UserId).Distinct());
                foreach (var stream in streams)
                {
                    if (users.TryGetValue(stream.UserId, out var user))
                        stream.ProfileImageUrl = user.ProfileImageUrl;
                }

                return streams;
            }
        }

        public static async Task<TwitchStreamInfo> GetStreamByLoginAsync(string login)
        {
            if (string.IsNullOrWhiteSpace(login))
                return null;

            if (!await EnsureAccessTokenAsync())
                return null;

            login = login.Trim().ToLowerInvariant();

            string usersUrl = $"{HelixBaseUrl}users?login={Uri.EscapeDataString(login)}";
            using (var userRequest = CreateHelixRequest(HttpMethod.Get, usersUrl))
            using (var userResponse = await Http.SendAsync(userRequest))
            {
                var userBody = await userResponse.Content.ReadAsStringAsync();
                if (!userResponse.IsSuccessStatusCode)
                    return null;

                string userId = null;
                string userName = login;
                string profileImageUrl = null;

                using (var doc = JsonDocument.Parse(userBody))
                {
                    if (!doc.RootElement.TryGetProperty("data", out var data) || data.GetArrayLength() == 0)
                        return null;

                    var user = data[0];
                    userId = user.GetProperty("id").GetString();
                    login = user.GetProperty("login").GetString();
                    userName = user.GetProperty("display_name").GetString();
                    profileImageUrl = user.GetProperty("profile_image_url").GetString();
                }

                string streamsUrl = $"{HelixBaseUrl}streams?user_login={Uri.EscapeDataString(login)}";
                using (var streamRequest = CreateHelixRequest(HttpMethod.Get, streamsUrl))
                using (var streamResponse = await Http.SendAsync(streamRequest))
                {
                    var streamBody = await streamResponse.Content.ReadAsStringAsync();
                    if (!streamResponse.IsSuccessStatusCode)
                        return null;

                    var streams = ParseStreams(streamBody);
                    if (streams.Count > 0)
                    {
                        streams[0].ProfileImageUrl = profileImageUrl;
                        return streams[0];
                    }

                    return new TwitchStreamInfo
                    {
                        UserId = userId,
                        UserLogin = login,
                        UserName = userName,
                        ProfileImageUrl = profileImageUrl,
                        Title = "Сейчас офлайн",
                        GameName = string.Empty,
                        ViewerCount = 0,
                        Language = string.Empty,
                        Tags = Array.Empty<string>()
                    };
                }
            }
        }

        private static async Task<bool> EnsureAccessTokenAsync()
        {
            if (!string.IsNullOrWhiteSpace(_accessToken) && DateTime.UtcNow < _tokenExpiresAtUtc)
                return true;

            string clientId = ConfigurationManager.AppSettings["TwitchClientId"];
            string clientSecret = ConfigurationManager.AppSettings["TwitchClientSecret"];
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                return false;

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["client_id"] = clientId,
                ["client_secret"] = clientSecret,
                ["grant_type"] = "client_credentials"
            });

            using (var response = await Http.PostAsync(TokenUrl, content))
            {
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($"Twitch OAuth: {(int)response.StatusCode} {body}");

                using (var doc = JsonDocument.Parse(body))
                {
                    _accessToken = doc.RootElement.GetProperty("access_token").GetString();
                    int expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();
                    _tokenExpiresAtUtc = DateTime.UtcNow.AddSeconds(Math.Max(60, expiresIn - 120));
                }
            }

            return !string.IsNullOrWhiteSpace(_accessToken);
        }

        private static async Task<string> GetDota2GameIdAsync()
        {
            if (!string.IsNullOrWhiteSpace(_dota2GameId))
                return _dota2GameId;

            string url = $"{HelixBaseUrl}games?name={Uri.EscapeDataString(Dota2GameName)}";
            using (var request = CreateHelixRequest(HttpMethod.Get, url))
            using (var response = await Http.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    throw new InvalidOperationException($"Twitch API (games): {(int)response.StatusCode} {body}");

                using (var doc = JsonDocument.Parse(body))
                {
                    if (!doc.RootElement.TryGetProperty("data", out var data) || data.GetArrayLength() == 0)
                        return null;

                    _dota2GameId = data[0].GetProperty("id").GetString();
                    return _dota2GameId;
                }
            }
        }

        private static async Task<Dictionary<string, (string Login, string DisplayName, string ProfileImageUrl)>> GetUsersByIdsAsync(
            IEnumerable<string> userIds)
        {
            var result = new Dictionary<string, (string, string, string)>();
            var ids = userIds.Where(id => !string.IsNullOrWhiteSpace(id)).Distinct().ToList();
            if (ids.Count == 0)
                return result;

            string query = string.Join("&", ids.Select(id => "id=" + Uri.EscapeDataString(id)));
            string url = $"{HelixBaseUrl}users?{query}";

            using (var request = CreateHelixRequest(HttpMethod.Get, url))
            using (var response = await Http.SendAsync(request))
            {
                var body = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    return result;

                using (var doc = JsonDocument.Parse(body))
                {
                    if (!doc.RootElement.TryGetProperty("data", out var data))
                        return result;

                    foreach (var user in data.EnumerateArray())
                    {
                        string id = user.GetProperty("id").GetString();
                        result[id] = (
                            user.GetProperty("login").GetString(),
                            user.GetProperty("display_name").GetString(),
                            user.GetProperty("profile_image_url").GetString());
                    }
                }
            }

            return result;
        }

        private static List<TwitchStreamInfo> ParseStreams(string json)
        {
            var list = new List<TwitchStreamInfo>();
            using (var doc = JsonDocument.Parse(json))
            {
                if (!doc.RootElement.TryGetProperty("data", out var data))
                    return list;

                foreach (var item in data.EnumerateArray())
                {
                    var tags = new List<string>();
                    if (item.TryGetProperty("tags", out var tagsElement))
                    {
                        foreach (var tag in tagsElement.EnumerateArray())
                            tags.Add(tag.GetString());
                    }

                    list.Add(new TwitchStreamInfo
                    {
                        StreamId = item.GetProperty("id").GetString(),
                        UserId = item.GetProperty("user_id").GetString(),
                        UserLogin = item.GetProperty("user_login").GetString(),
                        UserName = item.GetProperty("user_name").GetString(),
                        Title = item.GetProperty("title").GetString(),
                        GameName = item.GetProperty("game_name").GetString(),
                        ViewerCount = item.GetProperty("viewer_count").GetInt32(),
                        StartedAtUtc = DateTime.Parse(item.GetProperty("started_at").GetString()).ToUniversalTime(),
                        Language = item.GetProperty("language").GetString(),
                        ThumbnailUrl = item.GetProperty("thumbnail_url").GetString(),
                        Tags = tags.ToArray()
                    });
                }
            }

            return list;
        }

        private static HttpRequestMessage CreateHelixRequest(HttpMethod method, string url)
        {
            string clientId = ConfigurationManager.AppSettings["TwitchClientId"];
            var request = new HttpRequestMessage(method, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            request.Headers.Add("Client-Id", clientId);
            return request;
        }

        public static string GetCredentialsErrorMessage()
        {
            return "Укажите TwitchClientId и TwitchClientSecret в App.config (см. App.config.example).";
        }
    }
}
