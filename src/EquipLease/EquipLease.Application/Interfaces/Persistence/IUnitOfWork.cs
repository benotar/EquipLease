namespace EquipLease.Application.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IProductionFacilityRepository ProductionFacilityRepository { get; }
    IProcessEquipmentTypeRepository ProcessEquipmentTypeRepository { get; }
    IEquipmentPlacementContractRepository EquipmentPlacementContractRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}