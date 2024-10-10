using Surfs_Up.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{
    public class Wetsuit : ICartItem
    {
        public int WetsuitId {get; set;}

        public double Price {get; set;} = 149;

        //[Wetsuit_EnsureCorrectSizing]
        [Required] public SIZES Size {get; set;} = Wetsuit.SIZES.M;

        public List<Booking>? Bookings {get; set;}

        [Required]
        public GENDER Gender {get; set;}

        public enum GENDER
        {
            Mand, Kvinde
        }

        public enum SIZES
        {
            XS, S, M, L, XL, XXL, XXXL
        }
    }
}
