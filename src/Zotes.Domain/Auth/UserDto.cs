namespace Zotes.Domain.Auth;

public record UserDto(
    Guid Id,
    string Email,
    string? FirstName,
    string? LastName
);