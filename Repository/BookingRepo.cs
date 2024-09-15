using Surfs_Up.Models;

namespace Surfs_Up.Repository
{
    public class BookingRepo
    {
        public void SaveBookingToTextFile(Booking booking)
        {
            // Construct the file path correctly
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Persistence", "Booking.txt");

            try
            {
                // Use StreamWriter to append data to the text file
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    var items = new System.Text.StringBuilder();
                    foreach(var item in booking.BookingItems)
                    {
                        items.Append($" {item.Name},");
                    }
                    writer.WriteLine($"Startdate: {booking.StartDate}, Enddate {booking.EndDate}, CustomerName: {booking.Customer.Name}, CustomerEmail: {booking.Customer.Email}, Equipment: {items.ToString()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving booking data: {ex.Message}");
            }
        }
    }
}
