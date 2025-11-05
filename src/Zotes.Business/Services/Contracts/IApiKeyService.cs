using Zotes.Domain.Auth;

namespace Zotes.Business.Services.Contracts;

public interface IApiKeyService
{
    Task<ApiKeyResponse> IssueApiKeyAsync(
        Guid userId,
        TimeSpan? lifetime = null,
        CancellationToken cancellationToken = default
    );

    Task<UserDto?> ValidateApiKeyAsync(
        string apiKey,
        CancellationToken cancellationToken = default
    );
}