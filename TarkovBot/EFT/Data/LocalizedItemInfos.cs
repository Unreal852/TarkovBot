namespace TarkovBot.EFT.Data;

/// <summary>
/// Represents the localized name and short name of an item.
/// </summary>
public class LocalizedItemInfos : IIdentifiable
{
    public string Id        { get; set; }
    public string Name      { get; set; }
    public string ShortName { get; set; }
}