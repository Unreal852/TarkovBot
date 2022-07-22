using TarkovBot.EFT.Data;

namespace TarkovBot.Extensions;

public static class ArrayExtensions
{
    public static Dictionary<string, T> ToDictionary<T>(this T[] array) where T : IIdentifiable
    {
        if (array.Length == 0)
            return new Dictionary<string, T>();
        Dictionary<string, T> dictionary = new();
        foreach (T element in array)
            dictionary.Add(element.Id, element);
        return dictionary;
    }
}