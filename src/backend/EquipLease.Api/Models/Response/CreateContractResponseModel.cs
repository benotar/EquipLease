namespace EquipLease.Api.Models.Response;

public record CreateContractResponseModel(
    int Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);
