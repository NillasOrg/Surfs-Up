using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Indtast en gyldig Email")]
        [MaxLength(320)]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Indtast korrekt adgangskode")]
        [MaxLength(25, ErrorMessage = "Adgangskode må maks være 25 tegn")]
        [MinLength(8, ErrorMessage = "Adgangskode skal være minimum 8 tegn")]
        [DataType(DataType.Password)]
        public string Password {get; set;}
    }
}
