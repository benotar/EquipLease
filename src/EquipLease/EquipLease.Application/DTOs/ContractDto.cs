namespace EquipLease.Application.DTOs;

public record ContractDto(
    int Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);