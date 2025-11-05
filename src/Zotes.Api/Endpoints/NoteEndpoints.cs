using Zotes.Api.Config;
using Zotes.Api.Filters;
using Zotes.Domain.Notes;
using Zotes.Persistence.Entities;

namespace Zotes.Api.Endpoints;

public static class NoteEndpoints
{
    public static void MapNoteEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app
            .MapGroup($"{ApiVersioning.RoutePrefix}/notes")
            .AddEndpointFilter<ApiKeyRequiredFilter>()
            .AddEndpointFilter<ValidationFilter>()
            .WithTags("Notes");

        group.MapGet("", NoteHandlers.GetNotesAsync)
            .WithSummary("Get all Notes")
            .WithDescription("Get all Notes")
            .Produces<List<NoteDto>>();

        group.MapGet("/{id:guid}", NoteHandlers.GetNoteAsync)
            .WithSummary("Get Note by ID")
            .WithDescription("Get Note by ID")
            .Produces<NoteDto>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("", NoteHandlers.CreateNoteAsync)
            .WithSummary("Create a new Note")
            .WithDescription("Create a new Note")
            .Produces<NoteDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapPut("/{id:guid}", NoteHandlers.UpdateNoteAsync)
            .WithSummary("Update a Note by ID")
            .WithDescription("Update a Note by ID")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", NoteHandlers.DeleteNoteAsync)
            .WithSummary("Delete a Note by ID")
            .WithDescription("Delete a Note by ID")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}