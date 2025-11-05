using Zotes.Api.Config;

namespace Zotes.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup($"{ApiVersioning.RoutePrefix}/auth")
            .WithTags("Auth");

        group.MapPost("/register", AuthHandlers.RegisterAsync)
            .WithSummary("Register a new User")
            .WithDescription("Register a new User")
            .Produces(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPost("/login", AuthHandlers.LoginAsync)
            .WithSummary("Login a User")
            .WithDescription("Login a User")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/api-keys", AuthHandlers.IssueApiKeyAsync)
            .WithSummary("Issue a new API Key")
            .WithDescription("Issue a new API Key")
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status401Unauthorized);
    }
}