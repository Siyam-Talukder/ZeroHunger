using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BLL.Validations
{
    public class BdPhoneAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string phone)
            {
                if (phone.Length == 11 && phone.StartsWith("01") && phone.All(char.IsDigit))
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Phone must be exactly 11 numeric digits and start with '01'.");
            }
            return new ValidationResult("Invalid input type.");
        }
    }
}