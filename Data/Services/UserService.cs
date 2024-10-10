using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Surfs_Up.Data;
using Surfs_Up.Models;

public class UserService
{
    public UserService()
    {
        ApiContext.Initialize();
    }

    public async Task<bool> Login(string email, string password)
    {
        var loginData = new
        {
            Email = email,
            Password = password
        };

        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

        // Sender HTTP RequestMiddleware til at logge ind og få en JWT token
        using (HttpResponseMessage response = await ApiContext._apiClient.PostAsync("/api/auth/login", content))
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
        // Sender HTTP RequestMiddleware til at registrere
        using (HttpResponseMessage response = await ApiContext._apiClient.PostAsync("/api/auth/register", content))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }
    }
    
    public async Task<bool> Logout()
    {
        // Clear the JWT token from the session
        var contextAccessor = new HttpContextAccessor();
        contextAccessor.HttpContext.Session.Remove("JWToken");

        return true; // Indicate that the logout was successful
    }


    public async Task<User> GetUser()
    {
        var contextAccessor = new HttpContextAccessor();
        string token = contextAccessor.HttpContext.Session.GetString("JWToken");

        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        string name = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        string email = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        int id = Int32.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        return new User
        {
            Id = id,
            Name = name,
            Email = email
        };
    }

    public async Task<bool> IsLoggedIn()
    {
        var contextAccessor = new HttpContextAccessor();
        string token = contextAccessor.HttpContext.Session.GetString("JWToken");

        if (token != null)
        {
            return true;
        }

        return false;
    }
}