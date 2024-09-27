using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Indtast gyldig Brugernavn eller Email")]
        [MaxLength(20, ErrorMessage = "Brugernavn må maks være 20 tegn")]
        [DisplayName("Brugernavn eller Email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Indtast korrekt adgangskode")]
        [MaxLength(25, ErrorMessage = "Adgangskode må maks være 25 tegn")]
        [MinLength(8, ErrorMessage = "Adgangskode skal være minimum 8 tegn")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
