using System.Text.Json.Serialization;

namespace TarkovBot.Core.Data;

public class TaskObjectiveTaskStatus
{
    [JsonPropertyName("id")]          public string?  Id          { get; set; }
    [JsonPropertyName("type")]        public string   Type        { get; set; }
    [JsonPropertyName("description")] public string   Description { get; set; }
    [JsonPropertyName("maps")]        public Map[]    Maps        { get; set; }
    [JsonPropertyName("optional")]    public bool     Optional    { get; set; }
    [JsonPropertyName("task")]        public Task     Task        { get; set; }
    [JsonPropertyName("status")]      public string[] Status      { get; set; }
}