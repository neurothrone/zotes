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
            return TypedResults.Unauthorized();

        var notes = await service.GetAllAsync(userId, context.RequestAborted);
        return TypedResults.Ok(notes);
    }

    public static async Task<IResult> GetNoteAsync(
        Guid id,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return TypedResults.Unauthorized();

        var note = await service.GetAsync(id, userId, context.RequestAborted);
        return note is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(note);
    }

    public static async Task<IResult> CreateNoteAsync(
        NoteCreateRequest request,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return TypedResults.Unauthorized();

        var note = await service.CreateAsync(userId, request, context.RequestAborted);
        return TypedResults.Created($"/notes/{note.Id}", note);
    }

    public static async Task<IResult> UpdateNoteAsync(
        Guid id,
        NoteUpdateRequest request,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return TypedResults.Unauthorized();

        var updated = await service.UpdateAsync(id, userId, request, context.RequestAborted);
        return updated ? TypedResults.NoContent() : TypedResults.NotFound();
    }

    public static async Task<IResult> DeleteNoteAsync(
        Guid id,
        INoteService service,
        HttpContext context)
    {
        var userId = context.GetCurrentUserId();
        if (userId == Guid.Empty)
            return TypedResults.Unauthorized();

        var deleted = await service.DeleteAsync(id, userId, context.RequestAborted);
        return deleted ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}