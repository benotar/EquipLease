﻿using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Application.Interfaces.Persistence;

public interface IDbContext
{
    DbSet<ProductionFacility> ProductionFacilities { get; set; }
    DbSet<ProcessEquipmentType> ProcessEquipmentTypes { get; set; }
    DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}