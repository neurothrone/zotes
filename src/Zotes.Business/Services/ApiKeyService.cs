using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zotes.Business.Services.Contracts;
using Zotes.Domain.Auth;
using Zotes.Persistence.Data;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Services;

public class ApiKeyService(
    ZotesAppDbContext db,
    UserManager<User> userManager
) : IApiKeyService
{
    public async Task<ApiKeyResponse> IssueApiKeyAsync(
        Guid userId,
        TimeSpan? lifetime = null,
        CancellationToken cancellationToken = default)
    {
        var key = Convert.ToBase64String(Guid.NewGuid().ToByteArray()) + "." +
                  Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        DateTime? expires = lifetime.HasValue ? DateTime.UtcNow.Add(lifetime.Value) : null;
        var apiKey = new ApiKeyEntity
        {
            Key = key,
            UserId = userId,
            ExpiresAtUtc = expires
        };
        db.ApiKeys.Add(apiKey);
        await db.SaveChangesAsync(cancellationToken);
        return new ApiKeyResponse(key, expires);
    }

    public async Task<User?> ValidateApiKeyAsync(
        string apiKey,
        CancellationToken cancellationToken = default)
    {
        var key = await db.ApiKeys.FirstOrDefaultAsync(k => k.Key == apiKey, cancellationToken);
        if (key is null || key.IsExpired())
            return null;

        return await userManager.FindByIdAsync(key.UserId.ToString());
    }
}