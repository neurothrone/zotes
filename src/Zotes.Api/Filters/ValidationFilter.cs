using System.ComponentModel.DataAnnotations;

namespace Zotes.Api.Filters;

public class ValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        foreach (var argument in context.Arguments)
        {
            if (argument is null) continue;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(argument);

            if (!Validator.TryValidateObject(argument, validationContext, validationResults, true))
            {
                return Results.ValidationProblem(
                    validationResults.ToDictionary(
                        r => r.MemberNames.FirstOrDefault() ?? "Unknown",
                        r => new[] { r.ErrorMessage ?? "Validation failed" }
                    ));
            }
        }

        return await next(context);
    }
}