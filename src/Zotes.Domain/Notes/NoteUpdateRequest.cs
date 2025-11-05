using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zotes.Domain.Validation;

namespace Zotes.Domain.Notes;

public record NoteUpdateRequest
{
    [RequiredNonWhiteSpace(ErrorMessage = "Title is required and cannot be empty.")]
    [MaxLength(255)]
    [DefaultValue("System Failure Log #1 (AMENDED)")]
    public string Title { get; init; } = string.Empty;

    [MaxLength(4000)]
    [DefaultValue("Never mind. They're just buffering.")]
    public string? Content { get; init; }
}