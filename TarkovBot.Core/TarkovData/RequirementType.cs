using System.Text.Json.Serialization;

namespace TarkovBot.Core.TarkovData;

public enum RequirementType
{
    [JsonPropertyName("playerLevel")]    PlayerLevel,
    [JsonPropertyName("loyaltyLevel")]   LoyaltyLevel,
    [JsonPropertyName("questCompleted")] QuestCompleted,
    [JsonPropertyName("stationLevel")]   StationLevel
}