using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Application.Interfaces.DbContext;

public interface IDbContext : IDisposable
{
    DbSet<ProductionFacility> ProductionFacilities { get; set; }
    DbSet<ProcessEquipmentType> ProcessEquipmentTypes { get; set; }
    DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}