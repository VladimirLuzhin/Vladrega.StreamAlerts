using System.Runtime.Serialization;

namespace Vladrega.StreamAlerts.Core;

/// <summary>
/// Данные для алертинга
/// </summary>
[DataContract]
public record AlertData
{
    /// <summary>
    /// Имя отправителя
    /// </summary>
    [DataMember(Name = "senderName")]
    public string SenderName { get; init; }
    
    /// <summary>
    /// Имя кому задонатили
    /// </summary>
    [DataMember(Name = "name")]
    public string Name { get; init; }
    
    /// <summary>
    /// Текст доната
    /// </summary>
    [DataMember(Name = "text")]
    public string Text { get; init; }
    
    /// <summary>
    /// Представление ауидодорожки в base64 формате
    /// </summary>
    [DataMember(Name = "sound")]
    public string Sound { get; init; }

    /// <summary>
    /// .ctor
    /// </summary>
    /// <param name="senderName">От кого</param>
    /// <param name="name">Имя для кого пришел алерт</param>
    /// <param name="text">Текст алерта</param>
    /// <param name="sound">Аудиодорожка в base64 формате</param>
    public AlertData(string senderName, string name, string text, byte[] sound)
    {
        SenderName = senderName;
        Name = name;
        Text = text;
        Sound = Convert.ToBase64String(sound);
    }
}