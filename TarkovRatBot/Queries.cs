using TarkovRatBot.GraphQL;

namespace TarkovRatBot;

public class Queries
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
}