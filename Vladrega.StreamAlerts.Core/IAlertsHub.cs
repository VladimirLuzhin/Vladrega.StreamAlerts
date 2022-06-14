namespace Vladrega.StreamAlerts.Core;

/// <summary>
/// Абрстракция хаба, через который идет отправка эвентов
/// </summary>
public interface IAlertsHub
{
    /// <summary>
    /// Отправка алерта
    /// </summary>
    /// <param name="alertData">Данные, для отправки алерта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    Task SendAlertsAsync(AlertData alertData, CancellationToken cancellationToken);
}