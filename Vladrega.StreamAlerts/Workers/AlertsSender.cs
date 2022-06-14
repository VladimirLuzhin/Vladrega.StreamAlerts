using Vladrega.StreamAlerts.Connectors;
using Vladrega.StreamAlerts.Connectors.SignalR;
using Vladrega.StreamAlerts.Core;

namespace Vladrega.StreamAlerts.Workers;

/// <summary>
/// Фоновый сервис, который занимается доставкой алертов в хаб
/// </summary>
public class AlertsSender : BackgroundService
{
    private readonly AlertsBus _alertsBus;
    private readonly IAlertsHub _signalRAlertsHub;
    
    /// <summary>
    /// .ctor
    /// </summary>
    public AlertsSender(AlertsBus alertsBus, IAlertsHub signalRAlertsHub)
    {
        _alertsBus = alertsBus;
        _signalRAlertsHub = signalRAlertsHub;
    }

    /// <inheritdoc />
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.Run(async () =>
        {
            await foreach (var alert in _alertsBus.GetAlertsAsync(stoppingToken))
            {
                await _signalRAlertsHub.SendAlertsAsync(alert, stoppingToken);
            }
        }, stoppingToken);
    }
}