using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{
    public class RegistrationVeiwModel
    {
        [Required(ErrorMessage = "Indtast gyldigt navn")]
        [MaxLength(50, ErrorMessage = "Navn må maks være 50 tegn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Indtats venligst et gyldigt Email")]
        [MaxLength(50, ErrorMessage = "Email må maks være 50 tegn")]
        [EmailAddress]

        public string Email { get; set; }
        [Required(ErrorMessage = "Indtast gyldig Brugernavn")]
        [MaxLength(20, ErrorMessage = "Brugernavn må maks være 20 tegn")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Indtast korrekt adgangskode")]
        [MaxLength(25, ErrorMessage = "Adgangskode må maks være 25 tegn")]
        [MinLength(8, ErrorMessage = "Adgangskode skal være minimum 8 tegn")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Bekærft venligt adgangskode")]
        [DataType(DataType.Password)]

        public string ConfirmPassword { get; set; }
    }
}
