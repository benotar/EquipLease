namespace EquipLease.Api.Models.Response;

public record GetContractResponseModel(
    int Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);
