using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Zotes.Domain.Validation;

public sealed class StrongPasswordAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string s)
        {
            return new ValidationResult("Password is required.");
        }

        if (s.Length < 8)
        {
            return new ValidationResult("Password must be at least 8 characters long.");
        }

        if (!Regex.IsMatch(s, "[A-Z]"))
        {
            return new ValidationResult("Password must contain at least one uppercase letter.");
        }

        if (!Regex.IsMatch(s, "[a-z]"))
        {
            return new ValidationResult("Password must contain at least one lowercase letter.");
        }

        if (!Regex.IsMatch(s, "[0-9]"))
        {
            return new ValidationResult("Password must contain at least one digit.");
        }

        return ValidationResult.Success;
    }
}