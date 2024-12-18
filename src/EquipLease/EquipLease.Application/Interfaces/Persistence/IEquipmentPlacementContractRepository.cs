using EquipLease.Domain.Entities;

namespace EquipLease.Application.Interfaces.Persistence;

public interface IEquipmentPlacementContractRepository
{
    Task<IEnumerable<EquipmentPlacementContract>> GetAllAsync();
    Task<decimal> GetOccupiedAreaAsync(int productionFacilityId);
    void Add(EquipmentPlacementContract newContract);
}