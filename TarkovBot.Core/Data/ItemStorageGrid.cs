using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemStorageGrid
{
    [JsonPropertyName("width")]  public int Width  { get; set; }
    [JsonPropertyName("height")] public int Height { get; set; }
}