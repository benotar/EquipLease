using EquipLease.Application.Common;
using EquipLease.Application.DTOs;
using EquipLease.Application.Interfaces.Persistence;
using EquipLease.Application.Interfaces.Services;
using EquipLease.Domain.Entities;
using EquipLease.Domain.Enums;

namespace EquipLease.Application.Services;

public class ContractService : IContractService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContractService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<ContractDto>> CreateContractAsync(string productionFacilityCode,
        string processEquipmentTypeCode, int equipmentQuantity)
    {
        // Check if the production facility exists
        var existingProductionFacility = await _unitOfWork
            .ProductionFacilities
            .GetByCodeAsync(productionFacilityCode);

        if (existingProductionFacility is null)
        {
            return ErrorCode.ProductionFacilityNotFound;
        }

        // Check if the process equipment type exists
        var existingProcessEquipmentType = await _unitOfWork
            .ProcessEquipmentTypes
            .GetByCodeAsync(processEquipmentTypeCode);

        if (existingProcessEquipmentType is null)
        {
            return ErrorCode.ProcessEquipmentTypeNotFound;
        }

        // Check if the equipment quantity is valid
        if (equipmentQuantity < 1)
        {
            return ErrorCode.EquipmentQuantityNotValid;
        }

        // Calculate the total area occupied by all contracts for this production facility
        var allOccupiedAreaByContracts = await _unitOfWork
            .EquipmentPlacementContracts
            .GetOccupiedAreaAsync(existingProductionFacility.Id);

        // Calculate the required free area for placing production equipment in the production facility
        var requiredProductionFacilityArea = existingProcessEquipmentType.Area * equipmentQuantity;

        // Check whether there is enough area in the production facility to accommodate the equipment
        if (existingProductionFacility.StandardAreaForEquipment - allOccupiedAreaByContracts <
            requiredProductionFacilityArea)
        {
            return ErrorCode.NotEnoughFreeArea;
        }

        // Create new contract
        var newContract = new EquipmentPlacementContract
        {
            ProductionFacilityId = existingProductionFacility.Id,
            ProcessEquipmentTypeId = existingProcessEquipmentType.Id,
            NumberOfEquipmentUnits = equipmentQuantity
        };

        // Add contract to database
        _unitOfWork.EquipmentPlacementContracts.Add(newContract);

        // Update the amount of free area for the production facility
        _unitOfWork.ProductionFacilities.UpdateFreeArea(existingProductionFacility,
            requiredProductionFacilityArea);

        // Save changes
        await _unitOfWork.SaveChangesAsync();

        // Return Dto result
        return new ContractDto(
            newContract.Id,
            existingProductionFacility.Name,
            existingProcessEquipmentType.Name,
            equipmentQuantity
        );
    }

    public async Task<Result<IEnumerable<ContractDto>>> GetContractsAsync()
    {
        // Get a list of contracts, including navigation properties
        var contracts = await _unitOfWork
            .EquipmentPlacementContracts
            .GetAllAsync();

        // Return Dto result
        var contractsDto = contracts.Select(epc =>
            new ContractDto(
                epc.Id,
                epc.ProductionFacility.Name,
                epc.ProcessEquipmentType.Name,
                epc.NumberOfEquipmentUnits
            )).ToList();

        return contractsDto;
    }
}