using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TraderStanding
{
    [JsonPropertyName("trader")]   public Trader Trader   { get; set; }
    [JsonPropertyName("standing")] public float  Standing { get; set; }
}