using System.Text.Json;
using Azure.Storage.Queues;
using EquipLease.Application.Common;
using EquipLease.Application.DTOs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EquipLease.Application.Services;

public class EquipBackgroundService : BackgroundService
{
    private readonly ILogger<EquipBackgroundService> _logger;
    private readonly JsonSerializerOptions _options;
    private readonly QueueClient _queueClient;


    public EquipBackgroundService(ILogger<EquipBackgroundService> logger, JsonSerializerOptions options,
        QueueClient queueClient)
    {
        _logger = logger;
        _options = options;
        _queueClient = queueClient;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Reading from Azure Queue.");

            // Receive message from Azure Queue Storage
            var queueMessage = await _queueClient.ReceiveMessageAsync(
                TimeSpan.FromSeconds(30), stoppingToken);

            // Check if message exists
            if (queueMessage.Value is not null)
            {
                // Parse message
                var createContractResultData =
                    JsonSerializer.Deserialize<Result<ContractDto>>(queueMessage.Value.MessageText);

                // Log message
                _logger.LogInformation("Create contract result: {createContractResultData}",
                    JsonSerializer.Serialize(createContractResultData, _options));

                // Delete message from Azure Queue Storage
                await _queueClient.DeleteMessageAsync(queueMessage.Value.MessageId, queueMessage.Value.PopReceipt,
                    stoppingToken);
            }
            else
            {
                _logger.LogInformation("No new messages found.");
            }

            await Task.Delay(TimeSpan.FromSeconds(7), stoppingToken);
        }
    }
}