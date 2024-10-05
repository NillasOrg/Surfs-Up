using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Surfs_Up.Data;
using Surfs_Up.Models;

public class UserService
{
    private readonly HttpClient _httpClient;

    public UserService()
    {
        ApiContext.Initialize();
        _httpClient = ApiContext._apiClient; // Forudsætter, at _apiClient er en HttpClient-instans
    }

    public async Task<bool> Login(string email, string password)
    {
        var loginData = new
        {
            Email = email,
            Password = password
        };

        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

        // Sender HTTP Request til at logge ind og få en JWT token
        using (HttpResponseMessage response = await _httpClient.PostAsync("/api/auth/login", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)?["token"];

                // Opbevarer token i Session
                var contextAccessor = new HttpContextAccessor();
                contextAccessor.HttpContext.Session.SetString("JWToken", token);

                return true;
            }

            return false;
        }
    }

    public async Task<bool> Register(string name, string email, string password)
    {
        var registerData = new
        {
            Name = name,
            Email = email,
            Password = password
        };

        var content = new StringContent(JsonSerializer.Serialize(registerData), Encoding.UTF8, "application/json");

        // Sender HTTP Request til at registrere
        using (HttpResponseMessage response = await _httpClient.PostAsync("/api/auth/register", content))
        {
            return response.IsSuccessStatusCode; // Returner true, hvis registreringen var succesfuld
        }
    }

    public async Task<bool> Logout()
    {
        // Fjerner JWT-token fra session
        var contextAccessor = new HttpContextAccessor();
        contextAccessor.HttpContext.Session.Remove("JWToken");

        return true; // Indikerer at logout var succesfuld
    }

    public async Task<User> GetUser()
    {
        var contextAccessor = new HttpContextAccessor();
        string token = contextAccessor.HttpContext.Session.GetString("JWToken");

        if (string.IsNullOrEmpty(token))
        {
            return null; // Returner null hvis ikke logget ind
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        string name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        string email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        // Hent ID som string og forsøg at konvertere til int
        var idClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        // Kontroller om idClaim er null før konvertering
        if (idClaim == null || !int.TryParse(idClaim, out int id))
        {
            return null; // Hvis konverteringen fejler, returner null
        }

        return new User
        {
            Name = name,
            Email = email
        };
    }

    public async Task<bool> IsLoggedIn()
    {
        var contextAccessor = new HttpContextAccessor();
        string token = contextAccessor.HttpContext.Session.GetString("JWToken");

        return !string.IsNullOrEmpty(token); // Returner true hvis token eksisterer
    }
    public async Task<string> TestApiConnection()
    {
        try
        {
            var response = await _httpClient.GetAsync("/api/auth/test"); // Erstat med et eksisterende endpoint
            if (response.IsSuccessStatusCode)
            {
                return "API connection successful!";
            }
            else
            {
                return $"API connection failed: {response.StatusCode}";
            }
        }
        catch (Exception ex)
        {
            return $"API connection error: {ex.Message}";
        }
    }

}
