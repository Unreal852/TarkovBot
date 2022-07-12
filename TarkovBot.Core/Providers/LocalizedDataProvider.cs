using System.Collections.Concurrent;
using TarkovBot.Core.Data;
using TarkovBot.Core.GraphQL;

namespace TarkovBot.Core.Providers;

public abstract class LocalizedDataProvider<TValue> : DataProvider<LanguageCode, ConcurrentDictionary<string, TValue>>
        where TValue : IIdentifiable
{
    protected LocalizedDataProvider(GraphQlQuery query) : base(query)
    {
    }

    public DateTime UpdatedAt { get; private set; }

    public override async Task<int> UpdateCache()
    {
        var count = 0;
        count += await UpdateCache(LanguageCode.en);
        count += await UpdateCache(LanguageCode.fr);
        UpdatedAt = DateTime.UtcNow;
        return count;
    }

    public virtual async Task<int> UpdateCache(LanguageCode lang)
    {
        TarkovCore.WriteLine($"[CACHE] Caching localized ({lang.ToString().ToUpper()}) {typeof(TValue).Name}...", ConsoleColor.Yellow);
        TValue[]? elements = await Query.ExecuteAs<TValue[]>($"lang: {lang}");
        if (elements == null || elements.Length == 0)
        {
            TarkovCore.WriteLine($"[CACHE] Failed to cache {typeof(TValue).Name} !", ConsoleColor.Red);
            return 0;
        }

        if (!Cache.TryGetValue(lang, out ConcurrentDictionary<string, TValue>? innerCache))
            Cache.TryAdd(lang, innerCache ??= new());

        innerCache.Clear();
        foreach (TValue element in elements)
            innerCache.TryAdd(element.Id, element);

        TarkovCore.WriteLine($"[CACHE] Successfully cached {innerCache.Count} ({lang.ToString().ToUpper()}) {typeof(TValue).Name} !", ConsoleColor.Green);
        return innerCache.Count;
    }

    public IEnumerable<TValue> Where(LanguageCode lang, Predicate<TValue> predicate)
    {
        if (!Cache.TryGetValue(lang, out ConcurrentDictionary<string, TValue>? cache))
            yield break;
        foreach (KeyValuePair<string, TValue> pair in cache)
        {
            if (predicate(pair.Value))
                yield return pair.Value;
        }
    }

    public TValue? Single(LanguageCode lang, Predicate<TValue> predicate)
    {
        return Where(lang, predicate).FirstOrDefault();
    }

    public TValue? GetByKey(LanguageCode lang, string key)
    {
        if (GetByKey(lang) is { } innerCache && innerCache.TryGetValue(key, out TValue? value))
            return value;
        return default;
    }

    public override IEnumerable<ConcurrentDictionary<string, TValue>> Where(Predicate<ConcurrentDictionary<string, TValue>> predicate)
    {
        throw new NotSupportedException();
    }

    public override ConcurrentDictionary<string, TValue>? Single(Predicate<ConcurrentDictionary<string, TValue>> predicate)
    {
        throw new NotSupportedException();
    }
}