using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Surfs_Up.Models
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100, ErrorMessage = "Navn må maks være 50 tegn")]
        [Required(ErrorMessage = "Indtast gyldigt navn")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Indtats venligst et gyldigt Email")]
        [MaxLength(50, ErrorMessage = "Email må maks være 50 tegn")]
        public string Email { get; set; }
    }
}
