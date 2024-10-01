using Surfs_Up.Models.Validations;

namespace Surfs_Up.Models
{
    public class Wetsuit
    {
        public int WetsuitId { get; set; }

        public double Price { get; set; }

        [Wetsuit_EnsureCorrectSizing]
        public double Size { get; set; }

        public List<Booking>? Bookings { get; set; }

        public GENDER Gender { get; set; }

        public enum GENDER
        {
            Men, Women
        } 
    }
}
