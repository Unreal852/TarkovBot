namespace TarkovBot.EFT.Data;

/// <summary>
/// Represents the name and short name of an item.
/// This may be used later when storing multiple localized names within a single <see cref="ItemInfos"/>
/// </summary>
public class LocalizedItemInfos : IIdentifiable
{
    public string Id        { get; set; }
    public string Name      { get; set; }
    public string ShortName { get; set; }
}