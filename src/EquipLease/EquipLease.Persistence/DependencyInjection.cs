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
            // Set the password in an environment variable "Database__Password"
            var databasePassword = !string.IsNullOrEmpty(dbConfig.PASSWORD)
                ? dbConfig.PASSWORD
                : throw new ArgumentException("Database password in not valid!", dbConfig.PASSWORD);
            
            var filledConnectionString = string.Format(dbConfig.ConnectionStringPattern,
                databasePassword);
            
            options.UseSqlServer(filledConnectionString);

            // If there is no EF cache, then it improves EF performance.
            // To work with queries that change the state of an entity - use .AsTracking().
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<EquipDbContext>());
        
        return services;
    }
}