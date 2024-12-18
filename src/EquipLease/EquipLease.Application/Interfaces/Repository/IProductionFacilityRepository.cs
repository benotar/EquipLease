using EquipLease.Domain.Entities;

namespace EquipLease.Application.Interfaces.Repository;

public interface IProductionFacilityRepository
{
    Task<ProductionFacility?> GetByCodeAsync(string code);
    void UpdateFreeArea(ProductionFacility existingProductionFacility, decimal requiredProductionFacilityArea);
}