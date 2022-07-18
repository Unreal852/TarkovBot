namespace TarkovBot.EFT.Data.Raw;

public class TaskObjective
{
    public string?  Id          { get; set; }
    public TaskType Type        { get; set; }
    public string   Description { get; set; }
    public string   __typename  { get; set; }
    public IdOnly[] Maps        { get; set; }
    public bool     Optional    { get; set; }
    public IdOnly?  Item        { get; set; }

    // Objective Build Item
    public IdOnly[]?             ContainsAll { get; set; }
    public IdOnly[]?             ContainsOne { get; set; }
    public AttributeThreshold[]? Attributes  { get; set; }

    // Objective Experience
    public HealthEffect? HealthEffect { get; set; }

    // Objective Extract
    public string[]? ExitStatus { get; set; }
    public string[]? ZoneNames  { get; set; }

    // Objective Item 
    public int?  Count         { get; set; }
    public bool? FoundInRaid   { get; set; }
    public int?  DogTagLevel   { get; set; }
    public int?  MaxDurability { get; set; }
    public int?  MinDurability { get; set; }

    // Objective Mark
    public IdOnly? MarkerItem { get; set; }

    // Objective Player Level
    public int? PlayerLevel { get; set; }
}