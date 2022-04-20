using TarkovRatBot.GraphQL;

namespace TarkovRatBot;

public class Queries
{
    public static GraphQlQuery ItemsByNameQuery { get; private set; }
    public static GraphQlQuery AmmoQuery        { get; private set; }

    public static void InitQueries()
    {
        ItemsByNameQuery = new GraphQlQuery("ItemsByName");
        AmmoQuery = new GraphQlQuery("Ammo");
    }
}