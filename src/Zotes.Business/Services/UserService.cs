using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Zotes.Business.Services.Contracts;
using Zotes.Domain.Auth;
using Zotes.Persistence.Entities;

namespace Zotes.Business.Services;

public class UserService(
    UserManager<User> userManager,
    ILogger<UserService> logger
) : IUserService
{
    private readonly ILogger<UserService> _logger = logger;

    public async Task<(bool Success, string? Error, User? User)> RegisterAsync(
        RegisterRequest request,
        CancellationToken cancellationToken = default)
    {
        var existing = await userManager.FindByEmailAsync(request.Email);
        if (existing != null)
        {
            return (false, "Email is already registered", null);
        }

        var user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var error = string.Join("; ", result.Errors.Select(e => e.Description));
            return (false, error, null);
        }

        return (true, null, user);
    }

    public async Task<User?> ValidateCredentialsAsync(
        LoginRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return null;

        var ok = await userManager.CheckPasswordAsync(user, request.Password);
        return ok ? user : null;
    }

    public async Task<User?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await userManager.FindByIdAsync(id.ToString());
    }
}