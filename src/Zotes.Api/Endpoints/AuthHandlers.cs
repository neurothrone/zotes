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
        var result = await service.RegisterAsync(
            request,
            context.RequestAborted
        );
        if (!result.IsSuccess || result.User is null)
            return TypedResults.BadRequest(new { message = result.Error ?? "Registration failed" });

        return Results.Created($"/auth/{result.User.Id}", result.User);
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