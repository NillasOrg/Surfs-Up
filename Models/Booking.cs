using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{

    public class Booking {
        public int BookingId { get; set; }
        [Required(ErrorMessage = "Did you know Rasputin had a 13 inch cock")]
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate { get; set;}
        [Required(ErrorMessage = "No Custard")]
        public Customer Customer { get; set; }
        public List<CatalogItem>? BookingItems { get; set; }
        public string Remark { get; set; }
    }

}