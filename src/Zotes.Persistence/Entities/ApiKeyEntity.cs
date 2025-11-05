using System.ComponentModel.DataAnnotations;

namespace Zotes.Persistence.Entities;

public class ApiKeyEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    public string Key { get; set; } = string.Empty;

    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? ExpiresAtUtc { get; set; }

    public Guid UserId { get; set; }

    public bool IsExpired() => ExpiresAtUtc.HasValue && DateTime.UtcNow > ExpiresAtUtc.Value;
}