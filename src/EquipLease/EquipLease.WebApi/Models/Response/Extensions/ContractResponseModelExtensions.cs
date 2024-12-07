using EquipLease.Application.DTOs;

namespace EquipLease.WebApi.Models.Response.Extensions;

public static class ContractResponseModelExtensions
{
    public static GetContractResponseModel ToGetModel(this ContractDto dto)
    {
        return new GetContractResponseModel(
            dto.Id,
            dto.ProductionFacilityName,
            dto.ProcessEquipmentTypeName,
            dto.EquipmentQuantity
        );
    }
    
    public static CreateContractResponseModel ToCreateModel(this ContractDto dto)
    {
        return new CreateContractResponseModel(
            dto.Id,
            dto.ProductionFacilityName,
            dto.ProcessEquipmentTypeName,
            dto.EquipmentQuantity
        );
    }
}