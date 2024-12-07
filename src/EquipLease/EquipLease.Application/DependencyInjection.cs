using EquipLease.Application.Interfaces.Services;
using EquipLease.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EquipLease.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IContractService, ContractService>();

        return services;
    }
}