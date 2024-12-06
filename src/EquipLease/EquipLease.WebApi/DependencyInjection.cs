﻿using EquipLease.Application.Common;
using EquipLease.Application.Common.Converters;
using EquipLease.Application.Configurations;
using EquipLease.Domain.Enums;
using EquipLease.WebApi.Extensions;
using EquipLease.WebApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace EquipLease.WebApi;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add database configurations
        services.Configure<DatabaseConfiguration>(
            configuration.GetSection(DatabaseConfiguration.ConfigurationKey));
        
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
}