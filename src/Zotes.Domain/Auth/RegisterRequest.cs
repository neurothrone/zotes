using System.ComponentModel.DataAnnotations;
using Zotes.Domain.Validation;

namespace Zotes.Domain.Auth;

public record RegisterRequest
{
    [Required]
    [EmailAddress]
    public required string Email { get; init; }

    [Required]
    [StrongPassword]
    public required string Password { get; init; }

    [MaxLength(100)]
    public string? FirstName { get; init; }

    [MaxLength(100)]
    public string? LastName { get; init; }
}