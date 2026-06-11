using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Dota_2_Training_Platform.Functions
{
    public static class NetworkConnectivity
    {
        private static readonly HttpClient ProbeHttp = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(8)
        };

        private static readonly string[] ProbeUrls =
        {
            "https://api.opendota.com/api/constants/heroes",
            "https://www.google.com/generate_204"
        };

        public static async Task<bool> IsInternetAvailableAsync(CancellationToken cancellationToken = default)
        {
            foreach (string url in ProbeUrls)
            {
                if (await TryProbeUrlAsync(url, cancellationToken))
                    return true;
            }

            return false;
        }

        private static async Task<bool> TryProbeUrlAsync(string url, CancellationToken cancellationToken)
        {
            try
            {
                using (var headRequest = new HttpRequestMessage(HttpMethod.Head, url))
                using (var headResponse = await ProbeHttp.SendAsync(headRequest, cancellationToken))
                {
                    if (headResponse.IsSuccessStatusCode)
                        return true;
                }
            }
            catch
            {
                // HEAD может быть недоступен — пробуем GET.
            }

            try
            {
                using (var getResponse = await ProbeHttp.GetAsync(url, cancellationToken))
                {
                    return getResponse.IsSuccessStatusCode;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
