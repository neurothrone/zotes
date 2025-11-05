using Zotes.Domain.Notes;

namespace Zotes.Business.Services.Contracts;

public interface INoteService
{
    Task<List<NoteDto>> GetAllAsync(
        Guid userId,
        CancellationToken cancellationToken = default
    );

    Task<NoteDto?> GetAsync(
        Guid noteId,
        Guid userId,
        CancellationToken cancellationToken = default
    );

    Task<NoteDto> CreateAsync(
        Guid userId,
        NoteCreateRequest request,
        CancellationToken cancellationToken = default
    );

    Task<bool> UpdateAsync(
        Guid noteId,
        Guid userId,
        NoteUpdateRequest request,
        CancellationToken cancellationToken = default
    );

    Task<bool> DeleteAsync(
        Guid noteId,
        Guid userId,
        CancellationToken cancellationToken = default
    );
}