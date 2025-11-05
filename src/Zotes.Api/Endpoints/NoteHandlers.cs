using Zotes.Api.Extensions;
using Zotes.Business.Services.Contracts;
using Zotes.Domain.Notes;

namespace Zotes.Api.Endpoints;

public static class NoteHandlers
{
    public static async Task<IResult> GetNotesAsync(
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var notes = await service.GetAllAsync(userId, context.RequestAborted);
        return Results.Ok(notes);
    }

    public static async Task<IResult> GetNoteAsync(
        Guid id,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var note = await service.GetAsync(id, userId, context.RequestAborted);
        return note is null
            ? Results.NotFound()
            : TypedResults.Ok(note);
    }

    public static async Task<IResult> CreateNoteAsync(
        NoteInputRequest request,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var note = await service.CreateAsync(userId, request, context.RequestAborted);
        return Results.Created($"/notes/{note.Id}", note);
    }

    public static async Task<IResult> UpdateNoteAsync(
        Guid id,
        NoteInputRequest request,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var ok = await service.UpdateAsync(id, userId, request, context.RequestAborted);
        return ok ? Results.NoContent() : Results.NotFound();
    }

    public static async Task<IResult> DeleteNoteAsync(
        Guid id,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return Results.Unauthorized();

        var ok = await service.DeleteAsync(id, userId, context.RequestAborted);
        return ok ? Results.NoContent() : Results.NotFound();
    }
}