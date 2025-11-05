using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zotes.Domain.Validation;

namespace Zotes.Domain.Auth;

public record RegisterRequest
{
    [Required]
    [EmailAddress]
    [DefaultValue("john.doe@example.com")]
    public required string Email { get; init; }

    [Required]
    [StrongPassword]
    [DefaultValue("P@ssw0rdA")]
    public required string Password { get; init; }

    [MaxLength(100)]
    [DefaultValue("John")]
    public string? FirstName { get; init; }

    [MaxLength(100)]
    [DefaultValue("Doe")]
    public string? LastName { get; init; }
}