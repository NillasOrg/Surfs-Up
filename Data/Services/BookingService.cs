using System.Text;
using System.Text.Json;
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
        var content = new StringContent(JsonSerializer.Serialize(booking), Encoding.UTF8, "application/json");
        using (HttpResponseMessage response = await ApiContext._apiClient.PostAsync("/api/booking", content))
        {
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(JsonSerializer.Serialize(booking));
                var createdBooking = await response.Content.ReadFromJsonAsync<Booking>();
                return createdBooking;
            }
            Console.WriteLine(response.ReasonPhrase);
            throw new Exception(response.ReasonPhrase);
        }
    }

    public async Task<bool> Delete(int bookingId)
    {
        using (HttpResponseMessage response = await ApiContext._apiClient.DeleteAsync($"/api/booking/{bookingId}"))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }

}