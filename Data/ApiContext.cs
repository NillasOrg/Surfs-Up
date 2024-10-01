using System.Net;
using System.Net.Http.Headers;

namespace Surfs_Up.Data;

public static class ApiContext
{
    // HttpClient er det vi skal bruge til at kalde på vores API
    public static HttpClient _apiClient;

    public static void Initialize()
    {
        var handler = new HttpClientHandler
        {
            UseCookies = true, // Enables cookie handling
            CookieContainer = new CookieContainer() // Stores cookies
        };
        _apiClient = new HttpClient(handler);
        // Angiver adressen på vores API
        _apiClient.BaseAddress = new Uri("https://localhost:5005");
    }
}