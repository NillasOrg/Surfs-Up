using System.Text;
using System.Text.Json;
using Surfs_Up.Data;

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

        // Send POST request to login and receive the JWT token
        using (HttpResponseMessage response = await ApiContext._apiClient.PostAsync("/api/auth/login", content))
        {
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<Dictionary<string, string>>(responseContent)?["token"];

                // Store the token in the session
                var contextAccessor = new HttpContextAccessor();
                contextAccessor.HttpContext.Session.SetString("JWToken", token); 
                Console.WriteLine("Token stored in session: " + contextAccessor.HttpContext.Session.GetString("JWToken"));
                return true;
            }
            else
            {
                return false;
            }
        }
    }


    public void AddJwtToClient(string token)
    {
        ApiContext._apiClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
    }
    
}
public class LoginResponseModel
{
    public string Token { get; set; }
}