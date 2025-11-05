using System.ComponentModel.DataAnnotations;

namespace Zotes.Persistence.Entities;

public class NoteEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public Guid UserId { get; init; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(4000)]
    public string? Content { get; set; }

    public DateTime CreatedAtUtc { get; init; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}