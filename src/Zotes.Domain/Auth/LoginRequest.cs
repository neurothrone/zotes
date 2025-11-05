using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Zotes.Domain.Auth;

public record LoginRequest
{
    [Required]
    [EmailAddress]
    [DefaultValue("john.doe@example.com")]
    public required string Email { get; init; }

    [Required]
    [DefaultValue("P@ssw0rdA")]
    public required string Password { get; init; }
}