using Microsoft.AspNetCore.Mvc;
using Vladrega.StreamAlerts.Authentication;

namespace Vladrega.StreamAlerts.Controllers;

/// <summary>
/// Контрллер для получения ссылки на widget для отображения алертов
/// </summary>
[Route("api/widget")]
public class WidgetController : ControllerBase
{
    /// <summary>
    /// Получение ссылки на виджет с токеном для доступа к получению алертов
    /// </summary>
    /// <param name="userName">Имя пользователя для которого генерируется токен</param>
    [HttpGet]
    public IActionResult GetWidgetUrl([FromQuery] string userName)
    {
        return Ok($"http://localhost:3000?token={JwtOptions.GetJwt(userName)}");
    }
}