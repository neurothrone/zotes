using Zotes.Persistence.Entities;

namespace Zotes.Persistence.Repositories.Contracts;

public interface INoteRepository
{
    Task<List<NoteEntity>> GetAllAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    Task<NoteEntity?> GetAsync(
        Guid userId,
        Guid noteId,
        CancellationToken cancellationToken = default
    );

    Task<NoteEntity> AddAsync(
        NoteEntity note,
        CancellationToken cancellationToken = default
    );

    Task<bool> UpdateAsync(
        NoteEntity note,
        CancellationToken cancellationToken = default
    );

    Task<bool> DeleteAsync(
        Guid userId,
        Guid noteId,
        CancellationToken cancellationToken = default
    );
}