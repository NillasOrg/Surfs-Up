using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{

    public class Booking {
        public int BookingId { get; set; }
        [Required(ErrorMessage = "Invalid Start Date")]
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate { get; set;}
        [Required]
        public User User { get; set; }
        public List<Surfboard>? BookingItems { get; set; }
        public string Remark { get; set; }
    }

}