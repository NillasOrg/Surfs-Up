using Surfs_Up.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{
    public class Wetsuit : ICartItem
    {
        public int WetsuitId { get; set; }

        public double Price { get; set; } = 149;

        [Wetsuit_EnsureCorrectSizing]
        [Required]
        public double Size { get; set; }

        public List<Booking>? Bookings { get; set; }

        [Required]
        public GENDER Gender { get; set; }

        public enum GENDER
        {
            Men, Women
        } 
    }
}
