using System.Text.Json.Serialization;

namespace TarkovRatBot.Core.TarkovData;

public enum RequirementType
{
    [JsonPropertyName("playerLevel")]    PlayerLevel,
    [JsonPropertyName("loyaltyLevel")]   LoyaltyLevel,
    [JsonPropertyName("questCompleted")] QuestCompleted,
    [JsonPropertyName("stationLevel")]   StationLevel
}