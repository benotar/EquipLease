using EquipLease.Application.Common;
using EquipLease.Application.DTOs;
using EquipLease.Application.Interfaces.Persistence;
using EquipLease.Application.Interfaces.Services;
using EquipLease.Domain.Entities;
using EquipLease.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Application.Services;

public class ContractService : IContractService
{
    private readonly IDbContext _dbContext;

    public ContractService(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<ContractDto>> CreateContractAsync(string productionFacilityCode,
        string processEquipmentTypeCode, int equipmentQuantity)
    {
        // Check if the production facility exists
        var existingProductionFacility = await _dbContext.ProductionFacilities
            .AsTracking()
            .FirstOrDefaultAsync(pf => pf.Code.Equals(productionFacilityCode));

        if (existingProductionFacility is null)
        {
            return ErrorCode.ProductionFacilityNotFound;
        }

        // Check if the process equipment type exists
        var existingProcessEquipmentType = await _dbContext.ProcessEquipmentTypes
            .FirstOrDefaultAsync(pet => pet.Code.Equals(processEquipmentTypeCode));

        if (existingProcessEquipmentType is null)
        {
            return ErrorCode.ProcessEquipmentTypeNotFound;
        }

        // Check if the equipment quantity is valid
        if (equipmentQuantity < 1)
        {
            return ErrorCode.EquipmentQuantityNotValid;
        }

        // Calculate the total area occupied by the equipment with the contract
        var totalProcessEquipmentTypeArea = existingProcessEquipmentType.Area * equipmentQuantity;

        // Check whether there is enough area in the production facility to accommodate the equipment
        if (totalProcessEquipmentTypeArea > existingProductionFacility.StandardAreaForEquipment)
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
        _dbContext.EquipmentPlacementContracts.Add(newContract);

        // Update the amount of free area for the production facility
        existingProductionFacility.StandardAreaForEquipment -= totalProcessEquipmentTypeArea;
        existingProductionFacility.UpdatedAt = DateTime.UtcNow;

        // Save changes
        await _dbContext.SaveChangesAsync();

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
        var contracts = await _dbContext.EquipmentPlacementContracts
            .Include(epc => epc.ProductionFacility)
            .Include(epc => epc.ProcessEquipmentType)
            .ToListAsync();

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