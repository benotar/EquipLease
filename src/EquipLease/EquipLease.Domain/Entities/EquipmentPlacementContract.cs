using System.Text.Json.Serialization;

namespace EquipLease.Domain.Entities;

public class EquipmentPlacementContract : DbEntity
{
    public int ProductionFacilityId { get; set; }
    public int ProcessEquipmentTypeId { get; set; }
    public int NumberOfEquipmentUnits { get; set; }

    // Navigation properties
    [JsonIgnore] public ProductionFacility? ProductionFacility { get; set; }
    [JsonIgnore] public ProcessEquipmentType? ProcessEquipmentType { get; set; }
}