namespace EquipLease.Api.Models.Response;

public record CreateContractResponseModel(
    long Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);
