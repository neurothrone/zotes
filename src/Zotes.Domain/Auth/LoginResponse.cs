namespace Zotes.Domain.Auth;

public record LoginResponse(
    Guid UserId,
    string Email,
    string? FirstName,
    string? LastName
);