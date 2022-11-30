// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable ClassNeverInstantiated.Global

using System.Text.Json.Serialization;

// ReSharper disable MemberCanBePrivate.Global

namespace TarkovBot.Data;

public class TarkovItem : IJsonOnDeserialized
{
    public string      Id               { get; set; } = default!;
    public string?     Name             { get; set; }
    public string?     Description      { get; set; }
    public string?     WikiLink         { get; set; }
    public string?     InspectImageLink { get; set; }
    public int         BasePrice        { get; set; }
    public int?        Avg24HPrice      { get; set; }
    public int?        Width            { get; set; }
    public int?        Height           { get; set; }
    public int         PricePerSlots    { get; set; }
    public int         Slots            { get; set; } = 1;
    public TarkovAmmo? Ammo             { get; set; }

    [JsonPropertyName("types")]
    public TarkovItemType[] ItemTypes { get; set; }

    public void OnDeserialized()
    {
        Avg24HPrice ??= BasePrice;

        if (Width.HasValue && Height.HasValue)
            Slots = Width.Value * Height.Value;

        PricePerSlots = Avg24HPrice.Value / Slots;
    }
}