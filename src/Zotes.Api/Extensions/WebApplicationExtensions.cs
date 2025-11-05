using Zotes.Api.Endpoints;

namespace Zotes.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void ConfigureZotesMiddleware(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseStaticFiles();
        app.MapGet("/", context =>
        {
            context.Response.Redirect("/index.html");
            return Task.CompletedTask;
        });

        app.MapAuthEndpoints();
        app.MapNoteEndpoints();
    }
}