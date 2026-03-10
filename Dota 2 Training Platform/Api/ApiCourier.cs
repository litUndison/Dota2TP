using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Windows.Forms;
using Dota_2_Training_Platform.Models;

namespace Dota_2_Training_Platform
{
    public static class ApiCourier
    {
        //1204572532
        //public static List<DotaPlayerProfileModel> players = new List<DotaPlayerProfileModel>(); // на всякий случай, вдруг понадобится доп. инфа
        private static readonly HttpClient _apiHttpClient = new HttpClient();
        public static Dictionary<int, DotaHeroModel> Heroes = new Dictionary<int, DotaHeroModel>();
        public static Dictionary<int, DotaItemModel> ItemsById = new Dictionary<int, DotaItemModel>();

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

            var heroes = JsonSerializer.Deserialize<List<DotaHeroModel>>(json);

            Heroes.Clear();

            foreach (var hero in heroes)
            {
                Heroes[hero.id] = hero;

            }
        }

        public static string GetHeroImage(int heroId)
        {
            if (!Heroes.ContainsKey(heroId))
                return "";

            string imgPath = Heroes[heroId].img;
            return $"cdn.cloudflare.steamstatic.com{imgPath}";
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
            return $"cdn.cloudflare.steamstatic.com{imgPath}";
        }
    }

}
