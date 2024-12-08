namespace EquipLease.Application.Configurations;

public class AuthenticationConfiguration
{
    public static readonly string ConfigurationKey = "Authentication";

    public string ApiKey { get; set; }
    public string HeaderName { get; set; }
}