using EquipLease.Domain.Entities;

namespace EquipLease.Application.Interfaces.Persistence;

public interface IProductionFacilityRepository
{
    Task<ProductionFacility?> GetByCodeAsync(string code);
    void UpdateFreeAreaAsync(ProductionFacility existingProductionFacility, decimal totalProcessEquipmentTypeArea);
}