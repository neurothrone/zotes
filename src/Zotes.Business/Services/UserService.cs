using Microsoft.AspNetCore.Identity;
using Zotes.Business.Mappers;
using Zotes.Business.Services.Contracts;
using Zotes.Domain.Auth;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Services;

public class UserService(UserManager<UserEntity> userManager) : IUserService
{
    public async Task<RegisterResult> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            return RegisterResult.Failure("Email is already registered");

        var user = new UserEntity
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var identityResult = await userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            var error = string.Join("; ", identityResult.Errors.Select(e => e.Description));
            return RegisterResult.Failure(error);
        }

        return RegisterResult.Success(user.ToDto());
    }

    public async Task<UserDto?> ValidateCredentialsAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return null;

        var ok = await userManager.CheckPasswordAsync(user, request.Password);
        return ok ? user.ToDto() : null;
    }
}