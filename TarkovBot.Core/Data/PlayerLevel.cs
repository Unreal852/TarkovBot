using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class PlayerLevel
{
    [JsonPropertyName("level")] public int Level { get; set; }
    [JsonPropertyName("exp")]   public int Exp   { get; set; }
}