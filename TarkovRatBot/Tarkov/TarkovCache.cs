using System.Collections.Concurrent;

namespace TarkovRatBot.Tarkov;

public static class TarkovCache
{
    public static ConcurrentDictionary<string, ItemInfo> ItemsCache { get; } = new();


}