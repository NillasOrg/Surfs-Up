using System.ComponentModel.DataAnnotations;

namespace Surfs_Up.Models.Validations
{
    //public class Wetsuit_EnsureCorrectSizingAttribute : ValidationAttribute
    //{
    //    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    //    {
    //        Wetsuit wetsuit = validationContext.ObjectInstance as Wetsuit;

    //        if (wetsuit != null && !string.IsNullOrWhiteSpace(wetsuit.Gender.ToString()))
    //        {
    //            if(wetsuit.Gender.ToString().Equals("Men", StringComparison.OrdinalIgnoreCase) && wetsuit.Size < 8)
    //            {
    //                return new ValidationResult("For vådedragter til herre skal størrelsen være større eller lig med 8");
    //            }
    //            else if (wetsuit.Gender.ToString().Equals("Women", StringComparison.OrdinalIgnoreCase) && wetsuit.Size < 6)
    //            {
    //                return new ValidationResult("For vådedragter til damer skal størrelsen være større eller lig med 6");
    //            }
    //        }

    //        return ValidationResult.Success;
    //    }
    //}
}
