using EquipLease.Domain.Entities;

namespace EquipLease.Application.Interfaces.Persistence;

public interface IProcessEquipmentTypeRepository
{
    Task<ProcessEquipmentType?> GetByCodeAsync(string code);
}