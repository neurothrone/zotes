using Microsoft.EntityFrameworkCore;
using Zotes.Persistence.Data;
using Zotes.Persistence.Entities;
using Zotes.Persistence.Repositories.Contracts;

namespace Zotes.Persistence.Repositories;

public class ApiKeyRepository(ZotesAppDbContext db) : IApiKeyRepository
{
    public async Task<ApiKeyEntity> AddAsync(
        Guid userId,
        string key,
        DateTime? expiresAtUtc,
        CancellationToken cancellationToken = default)
    {
        var entity = new ApiKeyEntity
        {
            Key = key,
            UserId = userId,
            ExpiresAtUtc = expiresAtUtc
        };
        db.ApiKeys.Add(entity);
        await db.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public Task<ApiKeyEntity?> GetByKeyAsync(
        string key,
        CancellationToken cancellationToken = default)
    {
        return db.ApiKeys
            .AsNoTracking()
            .FirstOrDefaultAsync(k => k.Key == key, cancellationToken);
    }
}