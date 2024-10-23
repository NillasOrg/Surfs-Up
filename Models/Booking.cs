using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{

    public class Booking {
        public int Id {get; set;}
        [Required(ErrorMessage = "Invalid Start Date")]
        public DateTime? StartDate {get; set;}
        public DateTime? EndDate { get; set;}
        [Required]

        public User User {get; set;}
        public List<Surfboard>? Surfboards {get; set;}
        public List<Wetsuit>? Wetsuits {get; set;}
        public string Remark {get; set;}
    }

}