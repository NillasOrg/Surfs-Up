using System.Net.Http.Headers;

namespace Surfs_Up.Data;

public static class ApiContext
{
    // HttpClient er det vi skal bruge til at kalde på vores API
    public static HttpClient _apiClient;

    public static void Initialize()
    {
        _apiClient = new HttpClient();
        // Angiver adressen på vores API
        _apiClient.BaseAddress = new Uri("https://localhost:5005");
    }
}