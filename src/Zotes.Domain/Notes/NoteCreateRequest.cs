using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Zotes.Domain.Validation;

namespace Zotes.Domain.Notes;

public record NoteCreateRequest
{
    [RequiredNonWhiteSpace(ErrorMessage = "Title is required and cannot be empty.")]
    [MaxLength(255)]
    [DefaultValue("System Failure Log #1")]
    public string Title { get; init; } = string.Empty;

    [MaxLength(4000)]
    [DefaultValue("The machines are learning. Send help.")]
    public string? Content { get; init; }
}