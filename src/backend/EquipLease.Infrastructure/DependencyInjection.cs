using EquipLease.Application.Configurations;
using EquipLease.Application.Interfaces.DbContext;
using EquipLease.Application.Interfaces.Repository;
using EquipLease.Application.Interfaces.UnitOfWork;
using EquipLease.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace EquipLease.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration,
        IHostEnvironment environment)
    {
        var dbConfig = new DatabaseConfiguration();

        configuration.Bind(DatabaseConfiguration.ConfigurationKey, dbConfig);

        services.AddDbContext<EquipDbContext>(options =>
        {
            if (dbConfig.ConnectionString.IsNullOrEmpty())
            {
                throw new ArgumentException("The database connection string pattern is invalid!");
            }

            if (environment.IsDevelopment())
            {
                options.UseSqlServer(dbConfig.ConnectionString);
            }
            else
            {
                options.UseAzureSql(dbConfig.ConnectionString);
            }

            // If there is no EF cache, then it improves EF performance.
            // To work with queries that change the state of an entity - use .AsTracking().
            options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

        services.AddScoped<IDbContext>(provider =>
            provider.GetRequiredService<EquipDbContext>());

        // Add repositories
        services.AddScoped<IProductionFacilityRepository, ProductionFacilityRepository>();
        services.AddScoped<IProcessEquipmentTypeRepository, ProcessEquipmentTypeRepository>();
        services.AddScoped<IEquipmentPlacementContractRepository, EquipmentPlacementContractRepository>();

        // Add UoW
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
