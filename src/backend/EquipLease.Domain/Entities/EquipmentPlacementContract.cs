using System.Text.Json.Serialization;

namespace EquipLease.Domain.Entities;

public class EquipmentPlacementContract : Entity
{
    public long ProductionFacilityId { get; set; }
    public long ProcessEquipmentTypeId { get; set; }
    public int NumberOfEquipmentUnits { get; set; }

    // Navigation properties
    [JsonIgnore] public ProductionFacility? ProductionFacility { get; set; }
    [JsonIgnore] public ProcessEquipmentType? ProcessEquipmentType { get; set; }
}
