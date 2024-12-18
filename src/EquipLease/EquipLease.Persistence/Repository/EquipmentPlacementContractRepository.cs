using EquipLease.Application.Interfaces.DbContext;
using EquipLease.Application.Interfaces.Repository;
using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Persistence.Repository;

public class EquipmentPlacementContractRepository : IEquipmentPlacementContractRepository
{
    private readonly IDbContext _dbContext;

    public EquipmentPlacementContractRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<EquipmentPlacementContract>> GetAllAsync()
    {
        return await _dbContext.EquipmentPlacementContracts
            .Include(epc => epc.ProductionFacility)
            .Include(epc => epc.ProcessEquipmentType)
            .ToListAsync();
    }

    public async Task<decimal> GetOccupiedAreaAsync(int productionFacilityId)
    {
        return await _dbContext.EquipmentPlacementContracts
            .Where(pf => pf.ProductionFacilityId == productionFacilityId)
            .Include(epc => epc.ProductionFacility)
            .SumAsync(epc => epc.NumberOfEquipmentUnits * epc.ProcessEquipmentType.Area);
    }

    public void Add(EquipmentPlacementContract newContract)
    {
        _dbContext.EquipmentPlacementContracts.Add(newContract);
    }
}