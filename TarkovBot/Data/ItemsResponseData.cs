// ReSharper disable ClassNeverInstantiated.Global

using System.Text.Json.Serialization;

namespace TarkovBot.Data;

#nullable disable

public class ItemsResponseData : IJsonOnDeserialized
{
    public TarkovItem[] Items { get; set; }

    public void OnDeserialized()
    {
        Items ??= Array.Empty<TarkovItem>();
    }
}