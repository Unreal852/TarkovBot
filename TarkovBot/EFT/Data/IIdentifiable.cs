using System.Text.Json.Serialization;

namespace TarkovBot.EFT.Data;

public interface IIdentifiable
{
    [JsonPropertyName("id")] public string Id { get; }
}