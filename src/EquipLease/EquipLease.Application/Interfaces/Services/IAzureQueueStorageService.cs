namespace EquipLease.Application.Interfaces.Services;

public interface IAzureQueueStorageService
{
    Task SendMessageAsync<TMessage>(TMessage message);
}