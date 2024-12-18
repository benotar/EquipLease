using EquipLease.Application.Configurations;
using EquipLease.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EquipLease.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = new DatabaseConfiguration();

        configuration.Bind(DatabaseConfiguration.ConfigurationKey, dbConfig);

        services.AddDbContext<EquipDbContext>(options =>
        {
            // Set the password in an environment variable "Database__ConnectionStringPattern"
            var connectionStringPattern = dbConfig.ConnectionStringPattern =
                !string.IsNullOrEmpty(dbConfig.ConnectionStringPattern)
                    ? dbConfig.ConnectionStringPattern
                    : throw new ArgumentException("The database connection string pattern is invalid!",
                        dbConfig.ConnectionStringPattern);

            // Use SQL Server database with transient error resiliency enabled
            options.UseSqlServer(connectionStringPattern,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 10,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                });

            // If there is no EF cache, then it improves EF performance.
            // To work with queries that change the state of an entity - use .AsTracking().
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<EquipDbContext>());

        return services;
    }
}