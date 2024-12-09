namespace EquipLease.Application.Configurations;

public class AzureQueueStorageConfiguration
{
    public static readonly string ConfigurationKey = "AzureStorageQueue";

    public string ConnectionString { get; set; }
    public string QueueName { get; set; }

}