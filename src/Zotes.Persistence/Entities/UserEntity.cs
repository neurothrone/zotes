using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Zotes.Persistence.Entities;

public class UserEntity : IdentityUser<Guid>
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
}