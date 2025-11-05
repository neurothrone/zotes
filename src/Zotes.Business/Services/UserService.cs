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

        var userEntity = request.ToEntity();
        var identityResult = await userManager.CreateAsync(userEntity, request.Password);
        if (!identityResult.Succeeded)
        {
            var error = string.Join("; ", identityResult.Errors.Select(e => e.Description));
            return RegisterResult.Failure(error);
        }

        return RegisterResult.Success(userEntity.ToDto());
    }

    public async Task<UserDto?> ValidateCredentialsAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email);
        if (userEntity is null)
            return null;

        var isPasswordValid = await userManager.CheckPasswordAsync(userEntity, request.Password);
        return isPasswordValid ? userEntity.ToDto() : null;
    }
}