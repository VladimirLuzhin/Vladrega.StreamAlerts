using Microsoft.AspNetCore.Mvc;
using Vladrega.StreamAlerts.Controllers.Dto;
using Vladrega.StreamAlerts.Core;

namespace Vladrega.StreamAlerts.Controllers;

/// <summary>
/// Контроллер для отправки алертов
/// </summary>
[Route("api/alerts")]
public class AlertsController : ControllerBase
{
    private readonly AlertsBus _alertsBus;

    /// <summary>
    /// .ctor
    /// </summary>
    public AlertsController(AlertsBus alertsBus)
    {
        _alertsBus = alertsBus;
    }

    /// <summary>
    /// Метод обработки отправки алерта
    /// </summary>
    /// <param name="dto">Данные для алерта</param>
    /// <param name="cancellationToken">Токен отмены операции</param>
    [HttpPost]
    public async Task<IActionResult> AlertAsync([FromBody] AlertDto dto, CancellationToken cancellationToken)
    {
        // для примера указываем файл хардкодом и получаем байтики прям в конструкторе
        var file = await System.IO.File.ReadAllBytesAsync("./sound.wav", cancellationToken);

        await _alertsBus.AddEventAsync(new AlertData(dto.SenderName, dto.Name, dto.Text, file), cancellationToken);
        return Ok();
    }
}