using Zotes.Business.Services.Contracts;
using Zotes.Domain.Auth;

namespace Zotes.Api.Endpoints;

public static class AuthHandlers
{
    public static async Task<IResult> RegisterAsync(
        RegisterRequest request,
        IUserService service,
        HttpContext context)
    {
        var (success, error, user) = await service.RegisterAsync(
            request,
            context.RequestAborted
        );
        if (!success || user is null)
            return TypedResults.BadRequest(new { message = error ?? "Registration failed" });

        var response = new LoginResponse(user.Id, user.Email ?? string.Empty, user.FirstName, user.LastName);
        return Results.Created($"/auth/{user.Id}", response);
    }

    public static async Task<IResult> LoginAsync(
        LoginRequest request,
        IUserService service,
        HttpContext context)
    {
        var user = await service.ValidateCredentialsAsync(request, context.RequestAborted);
        if (user is null)
            return Results.Unauthorized();

        var resp = new LoginResponse(user.Id, user.Email ?? string.Empty, user.FirstName, user.LastName);
        return Results.Ok(resp);
    }

    public static async Task<IResult> IssueApiKeyAsync(
        LoginRequest request,
        IUserService userService,
        IApiKeyService apiService,
        HttpContext context)
    {
        var user = await userService.ValidateCredentialsAsync(request, context.RequestAborted);
        if (user is null)
            return Results.Unauthorized();

        var response = await apiService.IssueApiKeyAsync(user.Id, TimeSpan.FromDays(30), context.RequestAborted);
        return Results.Ok(response);
    }
}