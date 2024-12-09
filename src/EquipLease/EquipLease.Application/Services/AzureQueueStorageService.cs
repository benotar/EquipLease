using System.Text.Json;
using Azure.Storage.Queues;
using EquipLease.Application.Interfaces.Services;

namespace EquipLease.Application.Services;

public class AzureQueueStorageService : IAzureQueueStorageService
{
    private readonly QueueClient _queueClient;

    public AzureQueueStorageService(QueueClient queueClient) => _queueClient = queueClient;

    public async Task SendMessageAsync<TData>(TData data)
    {
        // Send message to Azure Queue Storage
        await _queueClient.SendMessageAsync(JsonSerializer.Serialize(data));
    }
}