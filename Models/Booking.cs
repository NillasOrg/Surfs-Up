
namespace Surfs_Up.Models
{

    public class Booking {
        public DateTime StartDate {get; set;}
        public DateTime EndDate { get; set;}
        public Customer Customer { get; set; }
        public List<CatalogItem>? BookingItems { get; set; }
        public string Remark { get; set; }
    }

}