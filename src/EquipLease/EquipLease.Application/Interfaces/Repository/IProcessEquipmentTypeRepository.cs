using EquipLease.Domain.Entities;

namespace EquipLease.Application.Interfaces.Repository;

public interface IProcessEquipmentTypeRepository
{
    Task<ProcessEquipmentType?> GetByCodeAsync(string code);
}