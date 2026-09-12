namespace EquipLease.Application.DTOs;

public record ContractDto(
    long Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);
