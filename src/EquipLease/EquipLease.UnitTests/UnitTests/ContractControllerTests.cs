using EquipLease.Application.Common;
using EquipLease.Application.DTOs;
using EquipLease.Application.Interfaces.Services;
using EquipLease.Domain.Enums;
using EquipLease.WebApi.Controllers;
using EquipLease.WebApi.Models.Request;
using Moq;

namespace EquipLease.UnitTests.UnitTests;

public class ContractControllerTests
{
    private readonly Mock<IContractService> _mockContractService;
    private readonly Mock<IAzureQueueStorageService> _queueStorageService;
    private readonly ContractController _controller;

    public ContractControllerTests()
    {
        _mockContractService = new Mock<IContractService>();
        _queueStorageService = new Mock<IAzureQueueStorageService>();
        _controller = new ContractController(_mockContractService.Object, _queueStorageService.Object);
    }

    [Fact]
    public async Task Get_WhenServiceReturnsContracts_ReturnsExpectedData()
    {
        // Arrange
        var mockServiceResponseDtoContracts = new List<ContractDto>
        {
            new(1, "Facility1", "Type1", 10),
            new(2, "Facility2", "Type2", 5),
            new(3, "Facility3", "Type3", 2),
        };

        var serviceResult = Result<IEnumerable<ContractDto>>.Success(mockServiceResponseDtoContracts);
        _mockContractService.Setup(service => service.GetContractsAsync()).ReturnsAsync(serviceResult);

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.True(result.IsSucceed);
        Assert.NotNull(result.Data);
        Assert.Null(result.ErrorCode);

        var responseData = result.Data.ToList();
        Assert.Equal(3, responseData.Count);
        Assert.Equal("Facility1", responseData[0].ProductionFacilityName);
        Assert.Equal("Type3", responseData[2].ProcessEquipmentTypeName);
        Assert.Equal(5, responseData[1].EquipmentQuantity);
    }

    [Fact]
    public async Task Create_WhenServiceReturnsContract_ReturnsExpectedData()
    {
        // Arrange
        var mockContractRequestData = new CreateContractRequestModel
        {
            ProductionFacilityCode = "PF001",
            ProcessEquipmentTypeCode = "PET003",
            EquipmentQuantity = 6
        };

        var mockContractServiceResponseData = new ContractDto(
            1,
            "PF001",
            "PET003",
            6
        );

        var serviceResult = Result<ContractDto>.Success(mockContractServiceResponseData);
        _mockContractService.Setup(service =>
                service.CreateContractAsync(mockContractRequestData.ProductionFacilityCode,
                    mockContractRequestData.ProcessEquipmentTypeCode, mockContractRequestData.EquipmentQuantity))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _controller.Create(mockContractRequestData);

        // Assert
        Assert.True(result.IsSucceed);
        Assert.NotNull(result.Data);
        Assert.Null(result.ErrorCode);

        var responseData = result.Data;
        Assert.Equal(6, responseData.EquipmentQuantity);
        Assert.Equal("PF001", responseData.ProductionFacilityName);
        Assert.Equal("PET003", responseData.ProcessEquipmentTypeName);
    }

    [Fact]
    public async Task Create_WhenRequestModelInvalid_ReturnsError()
    {
        // Arrange
        var mockContractRequestData = new CreateContractRequestModel
        {
            ProductionFacilityCode = "PF001",
            ProcessEquipmentTypeCode = "",
            EquipmentQuantity = 0
        };

        var mockServiceErrorCode = ErrorCode.MockErrorCode;

        var serviceResult = Result<ContractDto>.Error(mockServiceErrorCode);
        _mockContractService.Setup(service =>
                service.CreateContractAsync(mockContractRequestData.ProductionFacilityCode,
                    mockContractRequestData.ProcessEquipmentTypeCode, mockContractRequestData.EquipmentQuantity))
            .ReturnsAsync(serviceResult);

        // Act
        var result = await _controller.Create(mockContractRequestData);

        // Assert
        Assert.False(result.IsSucceed);
        Assert.NotNull(result.ErrorCode);
        Assert.Equal(ErrorCode.MockErrorCode, result.ErrorCode);
        Assert.Null(result.Data);
    }

    [Fact]
    public async Task Get_WhenServiceReturnsContracts_ReturnsEmptyData()
    {
        // Arrange
        var mockServiceResponseDtoContracts = new List<ContractDto>();

        var serviceResult = Result<IEnumerable<ContractDto>>.Success(mockServiceResponseDtoContracts);
        _mockContractService.Setup(service => service.GetContractsAsync()).ReturnsAsync(serviceResult);

        // Act
        var result = await _controller.Get();

        // Assert
        Assert.True(result.IsSucceed);
        Assert.NotNull(result.Data);
        Assert.Empty(result.Data);
        Assert.Null(result.ErrorCode);
    }

    [Fact]
    public async Task Create_WhenCalled_CallsServiceWithCorrectParameters()
    {
        // Arrange
        var mockContractRequestData = new CreateContractRequestModel
        {
            ProductionFacilityCode = "PF001",
            ProcessEquipmentTypeCode = "PET003",
            EquipmentQuantity = 6
        };

        var mockContractServiceResponseData = new ContractDto(
            1,
            "PF001",
            "PET003",
            6
        );

        var serviceResult = Result<ContractDto>.Success(mockContractServiceResponseData);
        _mockContractService.Setup(service =>
                service.CreateContractAsync(mockContractRequestData.ProductionFacilityCode,
                    mockContractRequestData.ProcessEquipmentTypeCode, mockContractRequestData.EquipmentQuantity))
            .ReturnsAsync(serviceResult);

        // Act
        await _controller.Create(mockContractRequestData);

        // Assert
        _mockContractService.Verify(service =>
                service.CreateContractAsync(mockContractRequestData.ProductionFacilityCode,
                    mockContractRequestData.ProcessEquipmentTypeCode, mockContractRequestData.EquipmentQuantity),
            Times.Once);
    }

    [Fact]
    public async Task Get_WhenCalled_CallsServiceOnce()
    {
        // Arrange
        var mockServiceResponseDtoContracts = new List<ContractDto>
        {
            new(1, "Facility1", "Type1", 10),
            new(2, "Facility2", "Type2", 5),
            new(3, "Facility3", "Type3", 2),
        };

        var serviceResult = Result<IEnumerable<ContractDto>>.Success(mockServiceResponseDtoContracts);
        _mockContractService.Setup(service => service.GetContractsAsync()).ReturnsAsync(serviceResult);

        // Act
        await _controller.Get();

        // Assert
        _mockContractService.Verify(service => service.GetContractsAsync(),
            Times.Once);
    }
}