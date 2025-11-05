using System.ComponentModel.DataAnnotations;
using Zotes.Domain.Validation;

namespace Zotes.Domain.Notes;

public record NoteInputRequest
{
    [RequiredNonWhiteSpace(ErrorMessage = "Title is required and cannot be empty.")]
    [MaxLength(255)]
    public string Title { get; init; } = string.Empty;

    [MaxLength(4000)]
    public string? Content { get; init; }
}