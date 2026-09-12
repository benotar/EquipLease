using EquipLease.Domain.Entities;

namespace EquipLease.Application.Interfaces.Repository;

public interface IEquipmentPlacementContractRepository
{
    Task<IEnumerable<EquipmentPlacementContract>> GetAllAsync();
    Task<decimal> GetOccupiedAreaAsync(long productionFacilityId);
    void Add(EquipmentPlacementContract newContract);
}
