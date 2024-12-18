using EquipLease.Application.Interfaces.Repository;

namespace EquipLease.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IProductionFacilityRepository ProductionFacilities { get; }
    IProcessEquipmentTypeRepository ProcessEquipmentTypes { get; }
    IEquipmentPlacementContractRepository EquipmentPlacementContracts { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}