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

        // нужен метод для получения матчей игрока

    }

}
