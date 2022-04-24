using TarkovRatBot.Core.GraphQL;

namespace TarkovRatBot.Core;

public class TarkovCore
{
    public static GraphQlQuery ItemsByNameQuery { get; private set; }
    public static GraphQlQuery ItemsByIdsQuery { get; private set; }
    public static GraphQlQuery AmmoQuery        { get; private set; }

    public static void InitQueries()
    {
        ItemsByNameQuery = new GraphQlQuery("ItemsByName");
        ItemsByIdsQuery = new GraphQlQuery("ItemsByIDs");
        AmmoQuery = new GraphQlQuery("Ammo");
    }
    
    public static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
    {
        Console.ForegroundColor = color;
        Console.WriteLine($"[{DateTime.UtcNow:g}] {message}");
        Console.ResetColor();
    }
}