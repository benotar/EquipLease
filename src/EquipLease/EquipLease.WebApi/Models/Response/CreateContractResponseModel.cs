namespace EquipLease.WebApi.Models.Response;

public record CreateContractResponseModel(
    int Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);