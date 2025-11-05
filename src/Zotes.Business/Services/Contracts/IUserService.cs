using Zotes.Domain.Auth;

namespace Zotes.Business.Services.Contracts;

public interface IUserService
{
    Task<RegisterResult> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default
    );

    Task<UserDto?> ValidateCredentialsAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default
    );
}