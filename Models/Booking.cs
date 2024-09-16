
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;


namespace Surfs_Up.Models
{

    public class Booking {
        [Key]
        public int BookingId {get; set;}
        public DateTime Date {get; set;}
        public Customer Customer { get; set; } 
        public List<CatalogItem>? BookingItems { get; set; }
        public string Remark { get; set; }
    }

}