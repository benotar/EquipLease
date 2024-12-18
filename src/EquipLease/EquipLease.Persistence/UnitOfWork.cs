using EquipLease.Application.Interfaces.Persistence;

namespace EquipLease.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly IDbContext _dbContext;
    private bool _disposed = false;

    public IProductionFacilityRepository ProductionFacilityRepository { get; }

    public IProcessEquipmentTypeRepository ProcessEquipmentTypeRepository { get; }

    public IEquipmentPlacementContractRepository EquipmentPlacementContractRepository { get; }

    public UnitOfWork(IDbContext dbContext, 
        IProductionFacilityRepository productionFacilityRepository, 
        IProcessEquipmentTypeRepository processEquipmentTypeRepository, 
        IEquipmentPlacementContractRepository equipmentPlacementContractRepository)
    {
        _dbContext = dbContext;
        ProductionFacilityRepository = productionFacilityRepository;
        ProcessEquipmentTypeRepository = processEquipmentTypeRepository;
        EquipmentPlacementContractRepository = equipmentPlacementContractRepository;
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