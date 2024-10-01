using Surfs_Up.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
namespace Surfs_Up.Data.Services;

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
            UserName = email,
            Password = password
        };

        var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

        // Send POST request to login and receive cookies
        using (HttpResponseMessage response = await ApiContext._apiClient.PostAsync("/api/auth/login", content))
        {
            if (response.IsSuccessStatusCode)
            {
                // On successful login, cookies are automatically handled by HttpClient.
                return true;
            }
            else
            {
                // Handle invalid login
                return false;
            }
        }
    }
}