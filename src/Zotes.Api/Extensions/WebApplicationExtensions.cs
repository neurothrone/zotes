using Zotes.Api.Endpoints;

namespace Zotes.Api.Extensions;

public static class WebApplicationExtensions
{
    public static void MapZotesEndpoints(this WebApplication app)
    {
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