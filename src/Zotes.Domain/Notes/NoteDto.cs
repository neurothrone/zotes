namespace Zotes.Domain.Notes;

public record NoteDto(
    Guid Id,
    string Title,
    string? Content,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc
);