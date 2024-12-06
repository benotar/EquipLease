using System.Text.Json.Serialization;

namespace EquipLease.Domain.Entities;

public class ProductionFacility : DbEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal StandardAreaForEquipment { get; set; }

    // Navigation property
    [JsonIgnore]
    public ICollection<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; } =
        new List<EquipmentPlacementContract>();
}