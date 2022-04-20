using System.Text.Json.Serialization;

namespace TarkovRatBot.Tarkov;

public class ItemPrice
{
    [JsonPropertyName("source")]       public string             ItemSourceName { get; set; }
    [JsonPropertyName("currency")]     public string             Currency       { get; set; }
    [JsonPropertyName("price")]        public int?               Price          { get; set; }
    [JsonPropertyName("requirements")] public PriceRequirement[] Requirements   { get; set; }
}