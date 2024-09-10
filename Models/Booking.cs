using Microsoft.VisualBasic;

namespace Surfs_Up.Models
{

    public class Booking {
        public DateTime Date {get; set;}
        public Customer Customer { get; set; }
        public CatalogItem CatalogItem { get; set; }
    }

}