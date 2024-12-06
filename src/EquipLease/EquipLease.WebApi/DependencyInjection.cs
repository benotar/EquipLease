using EquipLease.Application.Configurations;

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
}