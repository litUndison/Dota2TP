using Dota_2_Training_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Drawing;

namespace Dota_2_Training_Platform
{
    public static class ApiCourier
    {
        //1204572532
        //public static List<DotaPlayerProfileModel> players = new List<DotaPlayerProfileModel>(); // на всякий случай, вдруг понадобится доп. инфа
        private static readonly HttpClient _apiHttpClient = new HttpClient();
        public static Dictionary<int, DotaHeroModel> Heroes = new Dictionary<int, DotaHeroModel>();
        public static Dictionary<int, DotaItemModel> ItemsById = new Dictionary<int, DotaItemModel>();
        private static Dictionary<int, Image> itemImageCache = new Dictionary<int, Image>();
        private static HttpClient client = new HttpClient();

        public enum ApiResultStatus
        {
            Success,
            NotFound,
            NetworkError,
            InvalidResponse
        }
        public class ApiResult<T>
        {
            public ApiResultStatus Status { get; set; }
            public T Data { get; set; }
            public string ErrorMessage { get; set; }

            public bool IsSuccess => Status == ApiResultStatus.Success;
        }


        public static async Task<ApiResult<DotaPlayerProfileModel>> TryGetUserInfo(string steamId)
        {
            string url = $"https://api.opendota.com/api/players/{steamId}";

            try
            {
                var response = await _apiHttpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResult<DotaPlayerProfileModel>
                    {
                        Status = response.StatusCode == System.Net.HttpStatusCode.NotFound
                            ? ApiResultStatus.NotFound
                            : ApiResultStatus.NetworkError,
                        ErrorMessage = $"Server returned {response.StatusCode}"
                    };
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<DotaPlayerProfileModel>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (result?.profile == null)
                {
                    return new ApiResult<DotaPlayerProfileModel>
                    {
                        Status = ApiResultStatus.InvalidResponse,
                        ErrorMessage = "Profile data is missing"
                    };
                }

                return new ApiResult<DotaPlayerProfileModel>
                {
                    Status = ApiResultStatus.Success,
                    Data = result
                };
            }
            catch (HttpRequestException)
            {
                return new ApiResult<DotaPlayerProfileModel>
                {
                    Status = ApiResultStatus.NetworkError,
                    ErrorMessage = "No internet connection or server unavailable"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<DotaPlayerProfileModel>
                {
                    Status = ApiResultStatus.InvalidResponse,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static async Task<ApiResult<List<DotaMatchModel>>> TryGetPlayerMatches(string steamId, int limit = 10)
        {
            string url = $"https://api.opendota.com/api/players/{steamId}/matches?limit={limit}";

            try
            {
                var response = await _apiHttpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResult<List<DotaMatchModel>>
                    {
                        Status = response.StatusCode == System.Net.HttpStatusCode.NotFound
                            ? ApiResultStatus.NotFound
                            : ApiResultStatus.NetworkError,
                        ErrorMessage = $"Server returned {response.StatusCode}"
                    };
                }

                var json = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<List<DotaMatchModel>>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (result == null)
                {
                    return new ApiResult<List<DotaMatchModel>>
                    {
                        Status = ApiResultStatus.InvalidResponse,
                        ErrorMessage = "Match list is empty"
                    };
                }

                return new ApiResult<List<DotaMatchModel>>
                {
                    Status = ApiResultStatus.Success,
                    Data = result
                };
            }
            catch (HttpRequestException)
            {
                return new ApiResult<List<DotaMatchModel>>
                {
                    Status = ApiResultStatus.NetworkError,
                    ErrorMessage = "No internet connection or server unavailable"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<List<DotaMatchModel>>
                {
                    Status = ApiResultStatus.InvalidResponse,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static async Task<ApiResult<List<DotaMatchModel>>> TryGetPlayerMatchesInPeriod(
            string steamId,
            DateTime startDate,
            DateTime endDate,
            int pageLimit = 100,
            int maxFetchedMatches = 3000)
        {
            long? lessThanMatchId = null;
            var collected = new List<DotaMatchModel>();
            int fetched = 0;

            long startUnix = new DateTimeOffset(startDate).ToUnixTimeSeconds();
            long endUnix = new DateTimeOffset(endDate).ToUnixTimeSeconds();

            while (fetched < maxFetchedMatches)
            {
                string url = $"https://api.opendota.com/api/players/{steamId}/matches?limit={pageLimit}";
                if (lessThanMatchId.HasValue)
                {
                    url += $"&less_than_match_id={lessThanMatchId.Value}";
                }

                ApiResult<List<DotaMatchModel>> pageResult;
                try
                {
                    var response = await _apiHttpClient.GetAsync(url);
                    if (!response.IsSuccessStatusCode)
                    {
                        return new ApiResult<List<DotaMatchModel>>
                        {
                            Status = response.StatusCode == System.Net.HttpStatusCode.NotFound
                                ? ApiResultStatus.NotFound
                                : ApiResultStatus.NetworkError,
                            ErrorMessage = $"Server returned {response.StatusCode}"
                        };
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var page = JsonSerializer.Deserialize<List<DotaMatchModel>>(
                        json,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    pageResult = new ApiResult<List<DotaMatchModel>>
                    {
                        Status = ApiResultStatus.Success,
                        Data = page ?? new List<DotaMatchModel>()
                    };
                }
                catch (HttpRequestException)
                {
                    return new ApiResult<List<DotaMatchModel>>
                    {
                        Status = ApiResultStatus.NetworkError,
                        ErrorMessage = "No internet connection or server unavailable"
                    };
                }
                catch (Exception ex)
                {
                    return new ApiResult<List<DotaMatchModel>>
                    {
                        Status = ApiResultStatus.InvalidResponse,
                        ErrorMessage = ex.Message
                    };
                }

                var pageData = pageResult.Data;
                if (pageData.Count == 0)
                {
                    break;
                }

                fetched += pageData.Count;

                foreach (var match in pageData)
                {
                    if (match.StartTime >= startUnix && match.StartTime <= endUnix)
                    {
                        collected.Add(match);
                    }
                }

                long oldestStartTime = pageData.Min(m => m.StartTime);
                if (oldestStartTime < startUnix)
                {
                    break;
                }

                if (pageData.Count < pageLimit)
                {
                    break;
                }

                lessThanMatchId = pageData.Min(m => m.MatchId);
            }

            return new ApiResult<List<DotaMatchModel>>
            {
                Status = ApiResultStatus.Success,
                Data = collected
            };
        }

        public static async Task<ApiResult<DotaMatchDetailsModel>> TryGetMatch(long matchId)
        {
            string url = $"https://api.opendota.com/api/matches/{matchId}";

            try
            {
                var response = await _apiHttpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new ApiResult<DotaMatchDetailsModel>
                    {
                        Status = ApiResultStatus.NetworkError,
                        ErrorMessage = $"Server returned {response.StatusCode}"
                    };
                }

                var json = await response.Content.ReadAsStringAsync();

                var match = JsonSerializer.Deserialize<DotaMatchDetailsModel>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (match == null)
                {
                    return new ApiResult<DotaMatchDetailsModel>
                    {
                        Status = ApiResultStatus.InvalidResponse,
                        ErrorMessage = "Match data invalid"
                    };
                }

                // вычисляем сторону игрока
                foreach (var player in match.players)
                {
                    player.isRadiant = player.player_slot < 128;

                    // считаем KDA
                    player.kda = player.deaths == 0
                        ? player.kills + player.assists
                        : (double)(player.kills + player.assists) / player.deaths;
                }

                return new ApiResult<DotaMatchDetailsModel>
                {
                    Status = ApiResultStatus.Success,
                    Data = match
                };
            }
            catch (HttpRequestException)
            {
                return new ApiResult<DotaMatchDetailsModel>
                {
                    Status = ApiResultStatus.NetworkError,
                    ErrorMessage = "No internet connection or server unavailable"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<DotaMatchDetailsModel>
                {
                    Status = ApiResultStatus.InvalidResponse,
                    ErrorMessage = ex.Message
                };
            }
        }

        public static async Task LoadHeroes()
        {
            string url = "https://api.opendota.com/api/constants/heroes";

            var response = await _apiHttpClient.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            var heroes = JsonSerializer.Deserialize<Dictionary<string, DotaHeroModel>>(json);

            Heroes.Clear();

            foreach (var hero in heroes.Values)
            {
                Heroes[hero.id] = hero;
                Heroes[hero.id].img = hero.img.Replace(".png?", ".png");
            }
        }

        public static string GetHeroImage(int heroId)
        {
            if (!Heroes.ContainsKey(heroId))
                return "";

            string name = Heroes[heroId].name;
            name = name.Replace("npc_dota_hero_", "");
            return $"https://cdn.cloudflare.steamstatic.com/apps/dota2/images/dota_react/heroes/{name}.png";
        }


        public static async Task LoadItems()
        {
            string url = "https://api.opendota.com/api/constants/items";
            var response = await _apiHttpClient.GetAsync(url);
            string json = await response.Content.ReadAsStringAsync();

            // десериализация в Dictionary<string, DotaItemModel>
            var dict = JsonSerializer.Deserialize<Dictionary<string, DotaItemModel>>(json);

            ItemsById.Clear();
            foreach (var kvp in dict)
            {
                // ключ — это строковое название item, value.id — это числовой item id
                ItemsById[kvp.Value.id] = kvp.Value;
            }
        }

        // Получить название предмета
        public static string GetItemName(int itemId)
        {
            return ItemsById.ContainsKey(itemId)
                ? ItemsById[itemId].dname
                : $"Item {itemId}";
        }

        // Получить URL иконки предмета
        public static string GetItemImage(int itemId)
        {
            if (!ItemsById.ContainsKey(itemId))
                return ""; // или путь к картинке "not found"

            string imgPath = ItemsById[itemId].img;
            return $"https://cdn.steamstatic.com{imgPath}";
        }
        public static async Task<Image> GetItemImageAsync(int itemId)
        {
            if (itemId == 0) return null;

            if (itemImageCache.ContainsKey(itemId))
                return itemImageCache[itemId];

            try
            {

                    string url = GetItemImage(itemId);
                    var stream = await client.GetStreamAsync(url);
                    Image img = Image.FromStream(stream);

                    itemImageCache[itemId] = img;

                    return img;

            }
            catch
            {
                return null;
            }
        }
    }

}
