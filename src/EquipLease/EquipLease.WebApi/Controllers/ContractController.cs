using EquipLease.Application.Common;
using EquipLease.Application.Interfaces.Services;
using EquipLease.WebApi.Models.Request;
using EquipLease.WebApi.Models.Response;
using EquipLease.WebApi.Models.Response.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EquipLease.WebApi.Controllers;

public class ContractController : BaseController
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet("get")]
    public async Task<Result<IEnumerable<GetContractResponseModel>>> Get()
    {
        // Call contract service
        var getContractsResult = await _contractService.GetContractsAsync();

        // Check the result of the contract service call
        if (!getContractsResult.IsSucceed)
        {
            return getContractsResult.ErrorCode;
        }
        
        var getContractsData = getContractsResult.Data;

        // Return Model result
        return getContractsData.Select(contractDto => contractDto.ToGetModel()).ToList();
    }

    [HttpPost("create")]
    public async Task<Result<CreateContractResponseModel>> Create([FromBody] CreateContractRequestModel requestModel)
    {
        // Call contract service
        var createContractResult = await _contractService
            .CreateContractAsync(requestModel.ProcessEquipmentTypeCode,
                requestModel.ProcessEquipmentTypeCode, requestModel.EquipmentQuantity);

        // Check the result of the contract service call
        if (!createContractResult.IsSucceed)
        {
            return createContractResult.ErrorCode;
        }

        var createdContract = createContractResult.Data;

        // Return Model result
        return createdContract.ToCreateModel();
    }
}