using EquipLease.Application.Interfaces.Persistence;
using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Persistence.Repository;

public class ProductionFacilityRepository : IProductionFacilityRepository
{
    private readonly IDbContext _dbContext;

    public ProductionFacilityRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductionFacility?> GetByCodeAsync(string code)
    {
        return await _dbContext.ProductionFacilities
            .AsTracking()
            .FirstOrDefaultAsync(pf => pf.Code.Equals(code));
    }

    public void UpdateFreeArea(ProductionFacility existingProductionFacility, decimal requiredProductionFacilityArea)
    {
        existingProductionFacility!.StandardAreaForEquipment -= requiredProductionFacilityArea;
        
        existingProductionFacility.UpdatedAt = DateTime.UtcNow;
    }
}