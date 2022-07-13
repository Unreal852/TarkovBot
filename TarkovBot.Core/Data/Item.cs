using TarkovBot.Core.GraphQL.Attributes;

namespace TarkovBot.Core.Data;

[GraphQl("items")]
public class Item : IIdentifiable
{
    public string           Id                    { get; set; }
    public string?          Name                  { get; set; }
    public string?          NormalizedName        { get; set; }
    public string?          ShortName             { get; set; }
    public int              BasePrice             { get; set; }
    public DateTime?        Updated               { get; set; }
    public int              Width                 { get; set; }
    public int              Height                { get; set; }
    public string           BackgroundColor       { get; set; }
    public string?          IconLink              { get; set; }
    public string           IconLinkFallback      { get; set; }
    public string?          WikiLink              { get; set; }
    public string?          ImageLink             { get; set; }
    public string           ImageLinkFallback     { get; set; }
    public string?          GridImageLink         { get; set; }
    public string           GridImageLinkFallback { get; set; }
    public ItemType[]       Types                 { get; set; }
    public int?             Avg24hPrice           { get; set; }
    public string?          Link                  { get; set; }
    public int?             LastLowPrice          { get; set; }
    public float?           ChangeLast48h         { get; set; }
    public float?           ChangeLast48hPercent  { get; set; }
    public int?             Low24hPrice           { get; set; }
    public int?             High24hPrice          { get; set; }
    public int?             LastOfferCount        { get; set; }
    public ItemPrice[]?     SellFor               { get; set; }
    public ItemPrice[]?     BuyFor                { get; set; }
    public ContainedItem[]? ContainsItems         { get; set; }
    public ItemCategory[]   Categories            { get; set; }
    public float?           Weight                { get; set; }
    public IdOnly[]         UsedInTasks           { get; set; }
    public IdOnly[]         ReceivedFromTasks     { get; set; }
    public IdOnly[]         BartersFor            { get; set; }
    public IdOnly[]         BartersUsing          { get; set; }
    public IdOnly[]         CraftsFor             { get; set; }
    public IdOnly[]         CraftsUsing           { get; set; }
}