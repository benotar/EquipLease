namespace EquipLease.Domain.Enums;

public enum ErrorCode
{
    ProductionFacilityNotFound,
    ProcessEquipmentTypeNotFound,
    EquipmentQuantityNotValid,
    NotEnoughFreeArea,
    InvalidModel,
    MockErrorCode,
    ApiKeyMissing,
    InvalidApiKey,
    UnexpectedError
}