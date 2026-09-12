namespace EquipLease.Api.Models.Response;

public record GetContractResponseModel(
    long Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);
