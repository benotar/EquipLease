using Azure;
using Azure.Storage.Queues.Models;

namespace EquipLease.Application.Interfaces.Services;

public interface IAzureQueueStorageService
{
    Task SendMessageAsync<TMessage>(TMessage message);
    Task<Response<QueueMessage>?> GetQueueMessageAsync(CancellationToken stoppingToken);
    Task<Response> DeleteQueueMessageAsync(string messageId, string popReceipt, CancellationToken stoppingToken);
}