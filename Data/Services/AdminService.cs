using Surfs_Up.Models;

namespace Surfs_Up.Data.Services
{
    public class AdminService
    {

        public AdminService()
        {
            ApiContext.Initialize();
        }

        public async Task<List<Request>> GetAllRequestLogs()
        {
            using (HttpResponseMessage response = await ApiContext._apiClient.GetAsync("/api/admin"))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<Request> apiRequestLogs = await response.Content.ReadFromJsonAsync<List<Request>>();
                    return apiRequestLogs;
                }

                throw new Exception(response.ReasonPhrase);
            }
        }
    }
}
