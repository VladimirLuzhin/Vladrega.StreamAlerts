using System.Collections.Concurrent;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Vladrega.StreamAlerts.Core;

namespace Vladrega.StreamAlerts.Connectors.SignalR;

/// <summary>
/// Реализация <see cref="IAlertsHub"/> с использованием SignalR
/// </summary>
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class SignalRAlertsHub : Hub, IAlertsHub
{
    private const string UpdatesMethodString = "Updates";
    private readonly ConcurrentDictionary<string, HashSet<string>> _activeUsers = new();

    /// <inheritdoc />
    public override Task OnConnectedAsync()
    {
        _activeUsers.AddOrUpdate(Context.UserIdentifier, new HashSet<string>()
        {
            Context.ConnectionId
        }, (_, hashSet) =>
        {
            hashSet.Add(Context.ConnectionId);
            return hashSet;
        });
        
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (_activeUsers.TryGetValue(Context.UserIdentifier, out var connections))
            connections.Remove(Context.ConnectionId);
        
        return Task.CompletedTask;
    }

    /// <summary>
    /// Отправка алертов подключенным клиентам
    /// </summary>
    public async Task SendAlertsAsync(AlertData alertData, CancellationToken cancellationToken)
    {
        if (!_activeUsers.TryGetValue(alertData.Name, out var connections))
            return;

        foreach (var connection in connections)
        {
            await Clients.Client(connection).SendCoreAsync(UpdatesMethodString, new [] { alertData }, cancellationToken);    
        }
    }
}