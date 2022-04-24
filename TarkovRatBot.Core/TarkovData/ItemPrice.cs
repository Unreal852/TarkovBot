using System.Text.Json.Serialization;

// ReSharper disable ClassNeverInstantiated.Global

namespace TarkovRatBot.Core.TarkovData;

public class ItemPrice
{
    [JsonPropertyName("source")]       public string             ItemSourceName { get; set; }
    [JsonPropertyName("currency")]     public string             Currency       { get; set; }
    [JsonPropertyName("price")]        public int?               Price          { get; set; }
    [JsonPropertyName("requirements")] public PriceRequirement[] Requirements   { get; set; }
}