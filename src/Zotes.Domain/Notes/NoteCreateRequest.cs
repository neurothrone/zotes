using System.ComponentModel.DataAnnotations;

namespace Zotes.Domain.Notes;

public class NoteCreateRequest
{
    [Required]
    [MaxLength(255)]
    public required string Title { get; init; }
    
    [MaxLength(4000)]
    public string? Content { get; init; }
}