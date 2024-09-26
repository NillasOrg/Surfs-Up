using Surfs_Up.Models;

namespace Surfs_Up.Data.Services;

public class CatalogItemService
{
    public CatalogItemService()
    {
        ApiContext.Initialize();
    }

    public async Task<List<CatalogItem>> GetAll()
    {
        using (HttpResponseMessage response = await ApiContext._apiClient.GetAsync("/api/product"))
        {
            if (response.IsSuccessStatusCode)
            {
                List<CatalogItem> catalogItems = await response.Content.ReadFromJsonAsync<List<CatalogItem>>();
                return catalogItems;
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
    
    public async Task<CatalogItem> GetById(int id)
    {
        using (HttpResponseMessage response =  await ApiContext._apiClient.GetAsync($"/api/product/{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                CatalogItem catalogItem = await response.Content.ReadFromJsonAsync<CatalogItem>();
                return catalogItem;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}