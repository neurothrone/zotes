using System.ComponentModel.DataAnnotations;

namespace Zotes.Persistence.Entities;

public class ApiKeyEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    [Required]
    [MaxLength(64)]
    public required string Key { get; init; }

    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public DateTime? ExpiresAtUtc { get; init; }

    public Guid UserId { get; init; }

    public bool IsExpired() => ExpiresAtUtc.HasValue && DateTime.UtcNow > ExpiresAtUtc.Value;
}