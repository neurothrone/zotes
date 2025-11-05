using Zotes.Business.Services.Contracts;

namespace Zotes.Api.Filters;

public class ApiKeyRequiredFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var httpContext = context.HttpContext;
        if (!httpContext.Request.Headers.TryGetValue("X-Api-Key", out var stringValues))
            return Results.Unauthorized();

        var apiKey = stringValues.ToString();
        if (string.IsNullOrWhiteSpace(apiKey))
            return Results.Unauthorized();

        var service = httpContext.RequestServices.GetRequiredService<IApiKeyService>();
        var user = await service.ValidateApiKeyAsync(apiKey, httpContext.RequestAborted);
        if (user is null)
            return Results.Unauthorized();

        httpContext.Items["UserId"] = user.Id;
        return await next(context);
    }
}