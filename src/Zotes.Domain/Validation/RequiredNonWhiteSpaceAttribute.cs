using System.ComponentModel.DataAnnotations;

namespace Zotes.Domain.Validation;

public sealed class RequiredNonWhiteSpaceAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is string str && !string.IsNullOrWhiteSpace(str))
            return ValidationResult.Success;

        var memberName = validationContext.MemberName ?? "Field";
        return new ValidationResult(
            ErrorMessage ?? $"{memberName} cannot be empty or whitespace.",
            [validationContext.MemberName ?? string.Empty]
        );
    }
}