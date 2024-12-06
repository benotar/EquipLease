using System.Text.Json.Serialization;

namespace EquipLease.Domain.Entities;

public class ProcessEquipmentType : DbEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Area { get; set; }

    // Navigation property
    [JsonIgnore]
    public ICollection<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; } =
        new List<EquipmentPlacementContract>();
}