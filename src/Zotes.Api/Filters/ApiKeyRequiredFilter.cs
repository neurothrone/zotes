using Zotes.Business.Services.Contracts;

namespace Zotes.Api.Filters;

public class ApiKeyRequiredFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        if (!httpContext.Request.Headers.TryGetValue("X-Api-Key", out var values))
        {
            return Results.Unauthorized();
        }

        var apiKey = values.ToString();
        if (string.IsNullOrWhiteSpace(apiKey))
            return Results.Unauthorized();

        var validator = httpContext.RequestServices.GetRequiredService<IApiKeyService>();
        var user = await validator.ValidateApiKeyAsync(apiKey, httpContext.RequestAborted);
        if (user is null)
            return Results.Unauthorized();

        httpContext.Items["UserId"] = user.Id;
        return await next(context);
    }
}