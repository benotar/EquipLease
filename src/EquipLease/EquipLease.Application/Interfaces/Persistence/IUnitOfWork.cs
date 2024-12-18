﻿namespace EquipLease.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IProductionFacilityRepository ProductionFacilities { get; }
    IProcessEquipmentTypeRepository ProcessEquipmentTypes { get; }
    IEquipmentPlacementContractRepository EquipmentPlacementContracts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}