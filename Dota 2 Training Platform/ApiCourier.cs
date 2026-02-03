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
        public static List<DotaPlayerProfileModel> players = new List<DotaPlayerProfileModel>(); // на всякий случай, вдруг понадобится доп. инфа
        private static readonly HttpClient _apiHttpClient = new HttpClient();



        public static async Task<DotaPlayerProfileModel> TryGetUserInfo(string SteamID)
        {
            string url = $"https://api.opendota.com/api/players/{SteamID}";
            try
            {
                var response = await _apiHttpClient.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                if(json == "{\"error\":\"Not Found\"}")
                {
                    return null;
                }

                var result = JsonSerializer.Deserialize<DotaPlayerProfileModel>(json);
                if (result != null)
                {
                    bool found = false;
                    foreach (var player in players)
                    {
                        if (player.profile.steamid.ToString() == result.profile.steamid.ToString())
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        players.Add(result);
                        return result;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Profile {SteamID}. Error {ex.Message}");
            }
            return null;
        }

    }

}
