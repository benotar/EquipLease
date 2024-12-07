namespace EquipLease.WebApi.Models.Response;

public record GetContractResponseModel(
    int Id,
    string ProductionFacilityName,
    string ProcessEquipmentTypeName,
    int EquipmentQuantity
);