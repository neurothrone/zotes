using Microsoft.EntityFrameworkCore;
using Zotes.Persistence.Data;
using Zotes.Persistence.Entities;
using Zotes.Persistence.Repositories.Contracts;

namespace Zotes.Persistence.Repositories;

public class NoteRepository(ZotesAppDbContext db) : INoteRepository
{
    public Task<List<NoteEntity>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return db.Notes
            .AsNoTracking()
            .Where(n => n.UserId == userId)
            .OrderByDescending(n => n.CreatedAtUtc)
            .ToListAsync(cancellationToken);
    }

    public Task<NoteEntity?> GetAsync(Guid noteId, Guid userId, CancellationToken cancellationToken = default)
    {
        return db.Notes
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId, cancellationToken);
    }

    public async Task<NoteEntity> AddAsync(NoteEntity note, CancellationToken cancellationToken = default)
    {
        db.Notes.Add(note);
        await db.SaveChangesAsync(cancellationToken);
        return note;
    }

    public async Task<bool> UpdateAsync(NoteEntity note, CancellationToken cancellationToken = default)
    {
        db.Notes.Update(note);
        var result = await db.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid noteId, Guid userId, CancellationToken cancellationToken = default)
    {
        var existingNote =
            await db.Notes.FirstOrDefaultAsync(n => n.Id == noteId && n.UserId == userId, cancellationToken);
        if (existingNote is null)
            return false;

        db.Notes.Remove(existingNote);
        return await db.SaveChangesAsync(cancellationToken) > 0;
    }
}