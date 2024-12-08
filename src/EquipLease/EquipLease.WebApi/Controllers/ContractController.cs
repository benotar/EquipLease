using EquipLease.Application.Common;
using EquipLease.Application.Interfaces.Services;
using EquipLease.WebApi.Infrastructure;
using EquipLease.WebApi.Models.Request;
using EquipLease.WebApi.Models.Response;
using EquipLease.WebApi.Models.Response.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EquipLease.WebApi.Controllers;

/// <summary>
/// Controller responsible for managing contracts, including retrieving all contracts and creating new contracts.
/// </summary>
public class ContractController : BaseController
{
    private readonly IContractService _contractService;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ContractController"/> class.
    /// </summary>
    /// <param name="contractService">The contract service to be used for handling contract logic.</param>
    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }
    
    /// <summary>
    /// Retrieves all contracts from the system.
    /// </summary>
    /// <response code="200">Returns a list of all contracts in the system.</response>
    /// <response code="401">Unauthorized. API key is missing or invalid.</response>
    /// <returns>A list of contract response models.</returns>
    /// <remarks>
    /// **Example Request:**
    ///
    /// GET {application_url}/api/contract/get
    ///
    /// **Example Response:**
    ///
    /// ```json
    /// {
    ///   "data": [
    ///     {
    ///       "id": 1,
    ///       "productionFacilityName": "Facility 1",
    ///       "processEquipmentTypeName": "Equipment Type 3",
    ///       "equipmentQuantity": 3
    ///     },
    ///     {
    ///       "id": 2,
    ///       "productionFacilityName": "Facility 2",
    ///       "processEquipmentTypeName": "Equipment Type 5",
    ///       "equipmentQuantity": 5
    ///     },
    ///     {
    ///       "id": 3,
    ///       "productionFacilityName": "Facility 3",
    ///       "processEquipmentTypeName": "Equipment Type 1",
    ///       "equipmentQuantity": 1
    ///     }
    ///   ],
    ///   "errorCode": null,
    ///   "isSucceed": true
    /// }
    /// ```
    /// </remarks>
    [HttpGet("get")]
    [ProducesResponseType(typeof(Result<IEnumerable<GetContractResponseModel>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Creates a new contract in the system.
    /// </summary>
    /// <param name="requestModel">The create contract request model containing contract details.</param>
    /// <response code="200">Contract successfully created.</response>
    /// <response code="401">Unauthorized. API key is missing or invalid.</response>
    /// <response code="422">Validation error. The provided data is incorrect or incomplete.</response>
    /// <returns>A result containing the newly created contract's details.</returns>
    /// <remarks>
    /// **Example Request:**
    ///
    /// POST {application_url}/api/contract/create
    ///
    /// ```json
    /// {
    ///   "ProductionFacilityCode": "PF001",
    ///   "ProcessEquipmentTypeCode": "PET003",
    ///   "EquipmentQuantity": 3
    /// }
    /// ```
    ///
    /// **Example Response:**
    ///
    /// ```json
    /// {
    ///   "data": {
    ///       "id": 1,
    ///       "productionFacilityName": "Facility 1",
    ///       "processEquipmentTypeName": "Equipment Type 3",
    ///       "equipmentQuantity": 3
    ///   },
    ///   "errorCode": null,
    ///   "isSucceed": true
    /// }
    /// ```
    /// </remarks>
    [HttpPost("create")]
    [ProducesResponseType(typeof(Result<CreateContractResponseModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<None>), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(Result<CustomValidationProblemDetails>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<Result<CreateContractResponseModel>> Create([FromBody] CreateContractRequestModel requestModel)
    {
        // Call contract service
        var createContractResult = await _contractService
            .CreateContractAsync(requestModel.ProductionFacilityCode,
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