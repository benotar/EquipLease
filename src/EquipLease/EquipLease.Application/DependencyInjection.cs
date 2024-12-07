using System.Text.Json;
using EquipLease.Application.Common.Converters;
using EquipLease.Application.Interfaces.Services;
using EquipLease.Application.Services;
using EquipLease.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace EquipLease.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IContractService, ContractService>();

        var jsonOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new ServerResponseStringEnumConverter<ErrorCode>()
            },
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DictionaryKeyPolicy = JsonNamingPolicy.CamelCase
        };

        services.AddSingleton(jsonOptions);
        
        return services;
    }
}