using Zotes.Domain.Auth;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Services.Contracts;

public interface IApiKeyService
{
    Task<ApiKeyResponse> IssueApiKeyAsync(
        Guid userId,
        TimeSpan? lifetime = null,
        CancellationToken cancellationToken = default
    );

    Task<User?> ValidateApiKeyAsync(
        string apiKey,
        CancellationToken cancellationToken = default
    );
}