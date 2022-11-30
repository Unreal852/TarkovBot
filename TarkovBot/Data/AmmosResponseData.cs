// ReSharper disable ClassNeverInstantiated.Global

using System.Text.Json.Serialization;

namespace TarkovBot.Data;

#nullable disable

public class AmmosResponseData : IJsonOnDeserialized
{
    public TarkovAmmo[] Ammo { get; set; }

    public void OnDeserialized()
    {
        Ammo ??= Array.Empty<TarkovAmmo>();
    }
}