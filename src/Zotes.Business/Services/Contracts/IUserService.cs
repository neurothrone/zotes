using Zotes.Domain.Auth;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Services.Contracts;

public interface IUserService
{
    Task<RegisterResult> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<UserEntity?> ValidateCredentialsAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default
    );

    Task<UserEntity?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default
    );
}