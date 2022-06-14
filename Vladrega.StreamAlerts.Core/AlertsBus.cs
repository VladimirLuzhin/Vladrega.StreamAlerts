using System.Threading.Channels;

namespace Vladrega.StreamAlerts.Core;

/// <summary>
/// Внутреняя шина для передачи алертов и обеспечения порядка
/// </summary>
public class AlertsBus
{
    private readonly Channel<AlertData> _channel = Channel.CreateUnbounded<AlertData>();
    
    /// <summary>
    /// Добавить алерт в очередь на отправку
    /// </summary>
    /// <param name="alertData">Данные алерта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public async Task AddEventAsync(AlertData alertData, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(alertData, cancellationToken);
    }

    /// <summary>
    /// Подписка на получение алертов внутри приложения
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции</param>
    public IAsyncEnumerable<AlertData> GetAlertsAsync(CancellationToken cancellationToken)
    {   
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}