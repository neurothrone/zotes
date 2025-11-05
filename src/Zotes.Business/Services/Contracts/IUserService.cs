using Zotes.Domain.Auth;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Services.Contracts;

public interface IUserService
{
    Task<(bool Success, string? Error, User? User)> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<User?> ValidateCredentialsAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default
    );

    Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
}