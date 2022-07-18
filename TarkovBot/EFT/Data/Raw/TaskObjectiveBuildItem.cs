namespace TarkovBot.EFT.Data.Raw;

public class TaskObjectiveBuildItem : TaskObjective
{
    public IdOnly               Item        { get; set; }
    public IdOnly[]             ContainsAll { get; set; }
    public IdOnly[]             ContainsOne { get; set; }
    public AttributeThreshold[] Attributes  { get; set; }
}