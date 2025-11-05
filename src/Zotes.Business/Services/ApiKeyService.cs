using Microsoft.AspNetCore.Identity;
using Zotes.Business.Mappers;
using Zotes.Business.Services.Contracts;
using Zotes.Domain.Auth;
using Zotes.Persistence.Entities;
using Zotes.Persistence.Repositories.Contracts;

namespace Zotes.Business.Services;

public class ApiKeyService(
    IApiKeyRepository repository,
    UserManager<UserEntity> userManager
) : IApiKeyService
{
    public async Task<ApiKeyResponse> IssueApiKeyAsync(
        Guid userId,
        TimeSpan? lifetime = null,
        CancellationToken cancellationToken = default)
    {
        var firstPart = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var secondPart = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        var key = $"{firstPart}.{secondPart}";
        DateTime? expires = lifetime.HasValue ? DateTime.UtcNow.Add(lifetime.Value) : null;

        await repository.AddAsync(userId, key, expires, cancellationToken);
        return new ApiKeyResponse(key, expires);
    }

    public async Task<UserDto?> ValidateApiKeyAsync(
        string apiKey,
        CancellationToken cancellationToken = default)
    {
        var apiKeyEntity = await repository.GetByKeyAsync(apiKey, cancellationToken);
        if (apiKeyEntity is null || apiKeyEntity.IsExpired())
            return null;

        var userEntity = await userManager.FindByIdAsync(apiKeyEntity.UserId.ToString());
        return userEntity?.ToDto();
    }
}