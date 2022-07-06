using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public enum RequirementType
{
    [JsonPropertyName("playerLevel")]    playerLevel,
    [JsonPropertyName("loyaltyLevel")]   loyaltyLevel,
    [JsonPropertyName("questCompleted")] questCompleted,
    [JsonPropertyName("stationLevel")]   stationLevel,
}