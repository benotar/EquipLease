using System.ComponentModel.DataAnnotations;

namespace EquipLease.WebApi.Models.Request;

public class CreateContractRequestModel
{
    [Required(ErrorMessage = "Production facility code is required.")]
    [MinLength(3, ErrorMessage = "Production facility code must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "Production facility code cannot exceed 50 characters.")]
    public string ProductionFacilityCode { get; set; }
    
    [Required(ErrorMessage = "Process equipment type code is required.")]
    [MinLength(3, ErrorMessage = "Process equipment type code must be at least 3 characters long.")]
    [MaxLength(50, ErrorMessage = "Process equipment type code cannot exceed 50 characters.")]
    public string ProcessEquipmentTypeCode { get; set; } 
    
    [Required(ErrorMessage = "Equipment quantity is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Equipment quantity must be at least 1.")]
    public int EquipmentQuantity { get; set; }
}