using EquipLease.Application.Interfaces.DbContext;
using EquipLease.Application.Interfaces.Repository;
using EquipLease.Application.Interfaces.UnitOfWork;

namespace EquipLease.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbContext _dbContext;
    private bool _disposed = false;

    public IProductionFacilityRepository ProductionFacilities { get; }

    public IProcessEquipmentTypeRepository ProcessEquipmentTypes { get; }

    public IEquipmentPlacementContractRepository EquipmentPlacementContracts { get; }

    public UnitOfWork(IDbContext dbContext, 
        IProductionFacilityRepository productionFacilityRepository, 
        IProcessEquipmentTypeRepository processEquipmentTypeRepository, 
        IEquipmentPlacementContractRepository equipmentPlacementContractRepository)
    {
        _dbContext = dbContext;
        ProductionFacilities = productionFacilityRepository;
        ProcessEquipmentTypes = processEquipmentTypeRepository;
        EquipmentPlacementContracts = equipmentPlacementContractRepository;
    }


    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}