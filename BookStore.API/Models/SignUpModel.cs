using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace BookStore.API.Models
{
    public class SignUpModel
    {
        [Required]
        [NoSpecialCharactersOrNumbers]
        public string FirstName { get; set; }

        [Required]
        [NoSpecialCharactersOrNumbers]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [CustomEmailValidation]
        
        public string Email { get; set; }

        [Required]
        [Compare("ConfirmPassword")]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }

    // Validation attribute for checking special characters, numbers, and double spaces
    public class NoSpecialCharactersOrNumbersAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                // Convert the input value to a trimmed string
                var strValue = value.ToString().Trim();

                // Replace multiple spaces with a single space
                strValue = Regex.Replace(strValue, @"\s+", " ");

                // Check if the string contains any characters that are not letters or spaces
                if (Regex.IsMatch(strValue, @"[^a-zA-Z\s]"))
                {
                    return new ValidationResult("Field must not contain any special characters or numbers.");
                }

                // Check for double spaces (though the previous regex replacement handles most cases)
                if (Regex.IsMatch(strValue, @"\s\s"))
                {
                    return new ValidationResult("Field must not contain double spaces.");
                }

                // Set the cleaned and trimmed value back to the original property if writable
                var property = validationContext.ObjectType.GetProperty(validationContext.MemberName);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(validationContext.ObjectInstance, strValue);
                }
            }

            // Return success if all checks pass
            return ValidationResult.Success;
        }
    }

    // Custom email validation attribute
    public class CustomEmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                var email = value.ToString().Trim(); // Trim leading/trailing spaces

                // Check for a single '@' character
                if (email.Count(c => c == '@') != 1)
                {
                    return new ValidationResult("Email must contain only a single '@' character.");
                }

                // Check for at least 4 characters before '@'
                var parts = email.Split('@');
                if (parts[0].Length < 4)
                {
                    return new ValidationResult("Email must have at least 4 characters before '@'.");
                }


                return ValidationResult.Success;
            }
            return ValidationResult.Success;
        }
    }
}
