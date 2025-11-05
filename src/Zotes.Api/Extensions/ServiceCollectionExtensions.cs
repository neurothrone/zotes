using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Zotes.Api.Config;
using Zotes.Business.Services;
using Zotes.Business.Services.Contracts;
using Zotes.Persistence.Data;
using Zotes.Persistence.Entities;
using Zotes.Persistence.Repositories;
using Zotes.Persistence.Repositories.Contracts;

namespace Zotes.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddZotesDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ZotesAppDbContext>(options =>
        {
            options.UseInMemoryDatabase("ZotesDb");

#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });

        services.AddDbContext<ZotesIdentityDbContext>(options =>
        {
            options.UseInMemoryDatabase("ZotesDb");

#if DEBUG
            options.EnableSensitiveDataLogging();
#endif
        });
    }

    public static void AddZotesIdentity(this IServiceCollection services)
    {
        services
            .AddIdentityCore<UserEntity>(options =>
            {
                // Align configuration with the StrongPassword attribute
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ZotesIdentityDbContext>();

        services.AddAuthentication();
        services.AddAuthorization();
    }


    public static void AddZotesServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IApiKeyService, ApiKeyService>();
        services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
        services.AddScoped<INoteRepository, NoteRepository>();
        services.AddScoped<INoteService, NoteService>();
    }

    public static void AddZotesSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // API documentation
            options.SwaggerDoc(ApiVersioning.DocName, new OpenApiInfo
            {
                Title = "Zotes API",
                Description = "API for managing Notes with API keys.",
                Version = ApiVersioning.SemanticName
            });

            // API key header definition
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = $"Provide your API key in the {ApiHeaders.ApiKeyHeaderName} header",
                Name = ApiHeaders.ApiKeyHeaderName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "ApiKeyScheme"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}