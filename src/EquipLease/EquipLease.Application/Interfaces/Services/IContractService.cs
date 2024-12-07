using EquipLease.Application.Common;
using EquipLease.Application.DTOs;

namespace EquipLease.Application.Interfaces.Services;

public interface IContractService
{
    Task<Result<ContractDto>> CreateContractAsync(string productionFacilityCode,
        string processEquipmentTypeCode, int equipmentQuantity);

    Task<Result<IEnumerable<ContractDto>>> GetContractsAsync();
}