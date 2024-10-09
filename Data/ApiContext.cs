using System.Net;
using System.Net.Http.Headers;

namespace Surfs_Up.Data
{
    public static class ApiContext
    {
        public static HttpClient _apiClient;

        public static void Initialize()
        {
            var handler = new HttpClientHandler();
            _apiClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://localhost:5005") // Opdater med den korrekte API-adresse
            };
        }

        public static void SetToken(string token)
        {
            _apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
