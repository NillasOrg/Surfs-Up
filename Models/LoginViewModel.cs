using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Indtast gyldig Brugernavn")]
        [MaxLength(20, ErrorMessage = "Brugernavn må maks være 20 tegn")]

        public string UserName { get; set; }
        [Required(ErrorMessage = "Indtast korrekt adgangskode")]
        [MaxLength(25, ErrorMessage = "Adgangskode må maks være 25 tegn")]
        [MinLength(8, ErrorMessage = "Adgangskode skal være minimum 8 tegn")]
        [DataType(DataType.Password)]

        public string Password { get; set; }
    }
}
