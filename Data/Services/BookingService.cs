using System.Text;
using Newtonsoft.Json;
using Surfs_Up.Models;

namespace Surfs_Up.Data.Services;

public class BookingService
{
    public BookingService()
    {
        ApiContext.Initialize();
    }

    public async Task<List<Booking>> GetAll()
    {
        using (HttpResponseMessage response = await ApiContext._apiClient.GetAsync("/api/booking"))
        {
            if (response.IsSuccessStatusCode)
            {
                List<Booking> bookings = await response.Content.ReadFromJsonAsync<List<Booking>>();
                return bookings;
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
    
    public async Task<Booking> GetById(int id)
    {
        using (HttpResponseMessage response =  await ApiContext._apiClient.GetAsync($"/api/booking/{id}"))
        {
            if (response.IsSuccessStatusCode)
            {
                Booking booking = await response.Content.ReadFromJsonAsync<Booking>();
                return booking;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<Booking> Create(Booking booking)
    {
        using (HttpResponseMessage response = await ApiContext._apiClient.PostAsJsonAsync("/api/booking", booking))
        {
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Booking>().Result;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}