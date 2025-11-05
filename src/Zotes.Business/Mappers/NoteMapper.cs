using Zotes.Domain.Notes;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Mappers;

public static class NoteMapper
{
    public static NoteDto ToDto(this NoteEntity entity) => new(
        entity.Id,
        entity.Title,
        entity.Content,
        entity.CreatedAtUtc,
        entity.UpdatedAtUtc
    );
}