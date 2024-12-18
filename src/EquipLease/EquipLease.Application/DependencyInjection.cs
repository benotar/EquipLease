using System.Text.Json;
using Azure.Storage.Queues;
using EquipLease.Application.Common.Converters;
using EquipLease.Application.Configurations;
using EquipLease.Application.Interfaces.Services;
using EquipLease.Application.Services;
using EquipLease.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquipLease.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Add AzureQueueStorageService
        services.AddScoped<IAzureQueueStorageService, AzureQueueStorageService>();

        // Add ContractService
        services.AddScoped<IContractService, ContractService>();

        // Add JsonSerializerOptions 
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

    // Queue client to implement asynchronous background processor logging
    // after calling the contract creation method in the Contract Controller
    public static IServiceCollection AddConfiguredQueueClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Set the Azure Storage Queue connection string in an environment variable "AzureStorageQueue__ConnectionString"
        var storageConfig = new AzureQueueStorageConfiguration();

        configuration.Bind(AzureQueueStorageConfiguration.ConfigurationKey, storageConfig);

        var queueClient = new QueueClient(storageConfig.ConnectionString, storageConfig.QueueName);

        services.AddSingleton(queueClient);

        return services;
    }
}