using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ServerStatus
{
    [JsonPropertyName("generalStatus")]   public Status?          GeneralStatus   { get; set; }
    [JsonPropertyName("currentStatuses")] public Status[]?        CurrentStatuses { get; set; }
    [JsonPropertyName("messages")]        public StatusMessage[]? Messages        { get; set; }
}