using System.Collections.Concurrent;
using Serilog;
using TarkovBot.EFT.Data.Raw;
using TarkovBot.GraphQL;

namespace TarkovBot.EFT.Data.Provider;

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
        Log.Information("Successfully cached {Count} {Type}", count, typeof(TValue).Name);
        return count;
    }

    public virtual async Task<int> UpdateCache(LanguageCode lang)
    {
        TValue[]? elements = await Query.ExecuteAs<TValue[]>($"lang: {lang.ToString().ToLower()}");
        if (elements == null || elements.Length == 0)
        {
            Log.Error("Failed to cache {Type}, the results returned where null or empty", typeof(TValue).Name);
            return 0;
        }

        if (!Cache.TryGetValue(lang, out ConcurrentDictionary<string, TValue>? innerCache))
            Cache.TryAdd(lang, innerCache ??= new());

        innerCache.Clear();
        foreach (TValue element in elements)
            innerCache.TryAdd(element.Id, element);

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