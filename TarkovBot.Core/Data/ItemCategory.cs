using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class ItemCategory
{
    [JsonPropertyName("id")]     public string  Id     { get; set; }
    [JsonPropertyName("name")]   public string  Name   { get; set; }
    [JsonPropertyName("parent")] public IdOnly? Parent { get; set; }
}