namespace EquipLease.Application.Configurations;

public class DatabaseConfiguration
{
    public static readonly string ConfigurationKey = "Database";
    
    public string ConnectionStringPattern { get; set; }
}