using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Indtast gyldig navn")]
        [MaxLength(50, ErrorMessage = "Navn må maks være 50 tegn")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Indtats gyldig Email")]
        [MaxLength(50, ErrorMessage = "Email må maks være 50 tegn")]
        [EmailAddress]
        public string Email { get; set; }

        public string UserName { get; set; }
        [Required(ErrorMessage = "Indtast gylding adgangskode")]
        [MaxLength(25, ErrorMessage = "Adgangskode må maks være 25 tegn")]
        [MinLength(8, ErrorMessage = "Adgangskode skal være minimum 8 tegn")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bekræft venligst din adgangskode")]
        [Compare("Password", ErrorMessage = "Adgangskode matcher ikke")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
