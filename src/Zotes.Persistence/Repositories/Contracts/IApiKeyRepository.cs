using Zotes.Persistence.Entities;

namespace Zotes.Persistence.Repositories.Contracts;

public interface IApiKeyRepository
{
    Task<ApiKeyEntity> AddAsync(
        Guid userId,
        string key,
        DateTime? expiresAtUtc,
        CancellationToken cancellationToken = default
    );

    Task<ApiKeyEntity?> GetByKeyAsync(
        string key,
        CancellationToken cancellationToken = default
    );
}