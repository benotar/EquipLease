using System.Reflection;
using EquipLease.Application.Common;
using EquipLease.Application.Common.Converters;
using EquipLease.Application.Configurations;
using EquipLease.Domain.Enums;
using EquipLease.WebApi.Authentication;
using EquipLease.WebApi.Extensions;
using EquipLease.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace EquipLease.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add database configurations
        services.Configure<DatabaseConfiguration>(
            configuration.GetSection(DatabaseConfiguration.ConfigurationKey));

        // Add authentication configurations
        services.Configure<AuthenticationConfiguration>(
            configuration.GetSection(AuthenticationConfiguration.ConfigurationKey));

        // Add Azure Queue Storage configurations
        services.Configure<AzureQueueStorageConfiguration>(
            configuration.GetSection(AzureQueueStorageConfiguration.ConfigurationKey));

        return services;
    }

    // Add configured controllers
    public static IServiceCollection AddControllersWithConfiguredApiBehavior(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters
                    .Add(new ServerResponseStringEnumConverter<ErrorCode>()))
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.GetErrors();

                    var details = new CustomValidationProblemDetails
                    {
                        Type = "https://httpstatuses.com/422",
                        Title = "Validation Error",
                        Detail = "One or more validation errors occurred",
                        Instance = context.HttpContext.Request.Path,
                        Errors = errors
                    };

                    var result = new UnprocessableEntityObjectResult(
                        new Result<CustomValidationProblemDetails>
                        {
                            ErrorCode = ErrorCode.InvalidModel,
                            Data = details
                        });

                    result.ContentTypes.Add("application/json");

                    return result;
                };
            });

        return services;
    }

    // Add exception handler
    public static IServiceCollection AddExceptionHandlerWithProblemDetails(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        return services;
    }

    // Add API Key filter
    public static IServiceCollection AddApiKeyFilter(this IServiceCollection services)
    {
        services.AddScoped<ApiKeyAuthFilter>();

        return services;
    }

    // Add Swagger
    public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
    {
        var authConfig = new AuthenticationConfiguration();
        configuration.Bind(AuthenticationConfiguration.ConfigurationKey, authConfig);

        services.AddSwaggerGen(config =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath);

            config.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "The API Key to access the API",
                Type = SecuritySchemeType.ApiKey,
                Name = authConfig.HeaderName,
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            var requirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        },
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            };

            config.AddSecurityRequirement(requirement);
        });

        return services;
    }
}