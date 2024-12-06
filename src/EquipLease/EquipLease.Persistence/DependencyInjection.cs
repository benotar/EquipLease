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
        var dbConfiguration = new DatabaseConfiguration();

        configuration.Bind(DatabaseConfiguration.ConfigurationKey, dbConfiguration);

        services.AddDbContext<EquipDbContext>(options =>
        {
            var filledConnectionString = string.Format(dbConfiguration.ConnectionStringPattern);
            
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