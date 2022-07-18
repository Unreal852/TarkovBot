// ReSharper disable MemberCanBePrivate.Global

namespace TarkovBot.Core.GraphQL.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class GraphQlAttribute : Attribute
{
    public GraphQlAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}