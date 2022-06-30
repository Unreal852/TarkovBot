// ReSharper disable MemberCanBePrivate.Global

namespace TarkovRatBot.Core.GraphQL.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class GraphQlAttribute : Attribute
{
    public GraphQlAttribute(string name, string? args = null)
    {
        Name = name;
        Args = args;
    }

    public string  Name                { get; }
    public string? Args                { get; }
}