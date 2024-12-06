namespace EquipLease.Application.Configurations;

public class DatabaseConfiguration
{
    public static readonly string ConfigurationKey = "DATABASE";
    
    public string ConnectionStringPattern { get; set; }
    public string PASSWORD { get; set; }
}