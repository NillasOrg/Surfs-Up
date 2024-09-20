using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required(ErrorMessage = "Let me shake your hand")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Eating a meal... a succulent chinese meal")]
        public string Email { get; set; }
    }
}
