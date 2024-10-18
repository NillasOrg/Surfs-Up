using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using Surfs_Up.Data;
using Surfs_Up.Models;
using Surfs_Up.ViewModels;

public class UserService
{
    public UserService()
    {
        ApiContext.Initialize();
    }

    public async Task<LoginResponseModel> Login(LoginViewModel loginViewModel)
    {
        var response = await ApiContext._apiClient.PostAsJsonAsync("/login", loginViewModel );

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<LoginResponseModel>();
        }

        // Handle errors as needed
        var responseContent = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException($"Login failed. Status Code: {response.StatusCode}, Response: {responseContent}");
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
        // For at logge ud, sletter man bare tokens fra browseren
        var contextAccessor = new HttpContextAccessor();
        contextAccessor.HttpContext.Session.Remove("AccessToken");
        contextAccessor.HttpContext.Session.Remove("RefreshToken");
        contextAccessor.HttpContext.Session.Remove("Email");

        return true;
    }


    public async Task<User> GetUser()
    {
        var contextAccessor = new HttpContextAccessor();
        var accessToken = contextAccessor.HttpContext.Session.GetString("AccessToken");
        string? email = contextAccessor.HttpContext.Session.GetString("Email");

        ApiContext._apiClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);
        
        using (HttpResponseMessage response = await ApiContext._apiClient.GetAsync($"/api/user/{email}"))
        {
            if (response.IsSuccessStatusCode)
            {
                User user = await response.Content.ReadFromJsonAsync<User>();
                return user;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<bool> isLoggedIn()
    {
        var contextAccessor = new HttpContextAccessor();
        string token = contextAccessor.HttpContext.Session.GetString("AccessToken");

        if (token != null)
        {
            return true;
        }

        return false;
    }
}