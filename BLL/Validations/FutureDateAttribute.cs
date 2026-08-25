using System;
using System.ComponentModel.DataAnnotations;

namespace BLL.Validations
{
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                if (dateTime > DateTime.Now)
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult("Max preservation time must be in the future.");
            }
            return new ValidationResult("Invalid date format.");
        }
    }
}