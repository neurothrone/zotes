namespace Zotes.Domain.Auth;

public record ApiKeyResponse(
    string ApiKey,
    DateTime? ExpiresAtUtc
);