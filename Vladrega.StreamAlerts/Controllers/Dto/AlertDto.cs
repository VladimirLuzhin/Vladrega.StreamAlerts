using System.Runtime.Serialization;

namespace Vladrega.StreamAlerts.Controllers.Dto;

/// <summary>
/// Данные для алерта
/// </summary>
[DataContract]
public record AlertDto
{
    /// <summary>
    /// Имя пользователя, который отправил алерт
    /// </summary>
    [DataMember(Name = "senderName")]
    public string SenderName { get; init; }
    
    /// <summary>
    /// Пользователь, которому алертим
    /// </summary>
    [DataMember(Name = "name")]
    public string Name { get; init; }
    
    /// <summary>
    /// Текст для алерта
    /// </summary>
    [DataMember(Name = "text")]
    public string Text { get; init; }
}