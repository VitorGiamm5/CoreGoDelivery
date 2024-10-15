using System.ComponentModel.DataAnnotations;

namespace CoreGoDelivery.Domain.Validators.Attributes
{
    public class AdultAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthDate)
            {
                var age = DateTime.Today.Year - birthDate.Year;

                // Se o aniversário ainda não ocorreu neste ano, subtrair 1 da idade
                if (birthDate.Date > DateTime.Today.AddYears(-age))
                {
                    age--;
                }

                if (age >= 18)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The person must be at least 18 years old.");
                }
            }

            return new ValidationResult("Invalid birth date.");
        }
    }
}
