using Surfs_Up.Models;

namespace Surfs_Up.Data.Services
{
    public class AdminService
    {

        public AdminService()
        {
            ApiContext.Initialize();
        }

        public async Task<List<Request>> GetAllRequests()
        {
            using (HttpResponseMessage response = await ApiContext._apiClient.GetAsync("/api/admin"))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Request> requests = await response.Content.ReadFromJsonAsync<List<Request>>();
                    return requests;
                }

                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
