using Zotes.Api.Config;
using Zotes.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddZotesDatabase();
builder.Services.AddZotesIdentity();
builder.Services.AddZotesServices();
builder.Services.AddZotesSwagger();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            url: $"/swagger/{ApiVersioning.DocName}/swagger.json",
            name: $"Zotes API {ApiVersioning.SemanticName}"
        );
        // options.RoutePrefix = string.Empty; // Sets Swagger UI to be served at the root
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapZotesEndpoints();

app.Run();