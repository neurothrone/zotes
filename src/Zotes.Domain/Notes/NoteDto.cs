namespace Zotes.Domain.Notes;

public record NoteDto(
    Guid NoteId,
    Guid UserId,
    string Title,
    string? Content,
    DateTime CreatedAtUtc,
    DateTime? UpdatedAtUtc
);