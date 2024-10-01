using Surfs_Up.Models;

namespace Surfs_Up.Data.Services;

public class SurfboardService
{
    public SurfboardService()
    {
        ApiContext.Initialize();
    }

    public async Task<List<Surfboard>> GetAll()
    {
        using (HttpResponseMessage response = await ApiContext._apiClient.GetAsync("/api/surfboard"))
        {
            if (response.IsSuccessStatusCode)
            {
                List<Surfboard> surfboard = await response.Content.ReadFromJsonAsync<List<Surfboard>>();
                return surfboard;
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
    
    public async Task<Surfboard> GetById(int id)
    {
        using (HttpResponseMessage response =  await ApiContext._apiClient.GetAsync($"/api/surfboard/{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                Surfboard surfboard = await response.Content.ReadFromJsonAsync<Surfboard>();
                return surfboard;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}