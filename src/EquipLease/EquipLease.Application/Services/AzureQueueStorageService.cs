using System.Text.Json;
using Azure;
using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
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

    public async Task<Response<QueueMessage>?> GetQueueMessageAsync(CancellationToken stoppingToken)
    {
        return await _queueClient.ReceiveMessageAsync(
            TimeSpan.FromSeconds(30), stoppingToken);
    }

    public async Task<Response> DeleteQueueMessageAsync(string messageId, string popReceipt, CancellationToken stoppingToken)
    {
        return await _queueClient.DeleteMessageAsync(messageId, popReceipt,
            stoppingToken);
    }
}