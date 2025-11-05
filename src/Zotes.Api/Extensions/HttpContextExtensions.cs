namespace Zotes.Api.Extensions;

public static class HttpContextExtensions
{
    public static Guid GetCurrentUserId(this HttpContext httpContext)
    {
        return httpContext.Items.TryGetValue("UserId", out var val)
               && val is Guid gid
            ? gid
            : Guid.Empty;
    }
}