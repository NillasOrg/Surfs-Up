namespace Surfs_Up.Models;

public class BookingViewModel
{
    public IEnumerable<Booking> Bookings { get; set; }
    public IEnumerable<Customer> Customers { get; set; }
    public IEnumerable<CatalogItem> CatalogItems { get; set; }
}