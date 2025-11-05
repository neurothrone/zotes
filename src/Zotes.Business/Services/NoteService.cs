using Zotes.Business.Mappers;
using Zotes.Business.Services.Contracts;
using Zotes.Domain.Notes;
using Zotes.Persistence.Entities;
using Zotes.Persistence.Repositories.Contracts;

namespace Zotes.Business.Services;

public class NoteService(INoteRepository repository) : INoteService
{
    public async Task<List<NoteDto>> GetAllAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var entities = await repository.GetAllAsync(userId, cancellationToken);
        return entities
            .Select(e => e.ToDto())
            .ToList();
    }

    public async Task<NoteDto?> GetAsync(
        Guid noteId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetAsync(noteId, userId, cancellationToken);
        return entity?.ToDto();
    }

    public async Task<NoteDto> CreateAsync(
        Guid userId,
        string title,
        string? content,
        CancellationToken cancellationToken = default)
    {
        var entity = await repository.AddAsync(
            new NoteEntity
            {
                UserId = userId,
                Title = title,
                Content = content
            },
            cancellationToken
        );
        return entity.ToDto();
    }

    public async Task<bool> UpdateAsync(
        Guid noteId,
        Guid userId,
        string title,
        string? content,
        CancellationToken cancellationToken = default)
    {
        var existingNote = await repository.GetAsync(noteId, userId, cancellationToken);
        if (existingNote is null) 
            return false;
        
        existingNote.Title = title;
        existingNote.Content = content;
        existingNote.UpdatedAtUtc = DateTime.UtcNow;
        
        return await repository.UpdateAsync(existingNote, cancellationToken);
    }

    public Task<bool> DeleteAsync(
        Guid noteId,
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return repository.DeleteAsync(noteId, userId, cancellationToken);
    }
}