using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using TarkovBot.Core.GraphQL.Attributes;

namespace TarkovBot.Core.GraphQL;

public static class GraphQlQueryBuilder
{
    private static readonly BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty;

    public static GraphQlQuery? BuildQuery<T>()
    {
        Type type = typeof(T);
        var sw = Stopwatch.StartNew();
        TarkovCore.WriteLine($"[QUERY BUILDER] Building GraphQL query from type '{type.Name}'...", ConsoleColor.Yellow);
        if (!type.IsClass || type.IsAbstract)
            return null;
        var graphQlAttribute = type.GetCustomAttribute<GraphQlAttribute>();
        if (graphQlAttribute == null)
            return null;
        var builder = new StringBuilder("{").AppendLine();
        builder.Append(graphQlAttribute.Name).Append("(${ARGS})").AppendLine();

        BuildObject(type, builder);
        builder.Append('}');
        sw.Stop();
        TarkovCore.WriteLine($"[QUERY BUILDER] Successfully built GraphQL query from type '{type.Name}' in {sw.Elapsed.TotalMilliseconds:F}ms", ConsoleColor.Green);
        return new GraphQlQuery(graphQlAttribute.Name, builder.ToString());
    }

    private static void BuildObject(Type type, StringBuilder builder)
    {
        builder.Append('{').AppendLine();
        foreach (PropertyInfo property in GetProperties(type))
        {
            if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                continue;
            JsonPropertyNameAttribute? jsonName = property.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jsonName == null)
                continue;
            if (IsPrimitive(property.PropertyType))
                builder.Append(jsonName.Name).Append(',').AppendLine();
            else if (property.PropertyType.IsArray)
            {
                if (property.PropertyType.GetElementType()!.IsEnum)
                    builder.Append(jsonName.Name).Append(',').AppendLine();
                else
                {
                    builder.Append(jsonName.Name);
                    BuildObject(property.PropertyType.GetElementType()!, builder);
                }
            }
            else
            {
                builder.Append(jsonName.Name);
                BuildObject(property.PropertyType, builder);
            }
        }

        builder.Append('}');
    }

    private static PropertyInfo[] GetProperties<T>()
    {
        return GetProperties(typeof(T));
    }

    private static PropertyInfo[] GetProperties(Type type)
    {
        return type.GetProperties(BindingFlags).Where(p => p.GetCustomAttribute<CompilerGeneratedAttribute>() == null).ToArray();
    }

    private static bool IsPrimitive(Type type)
    {
        return type.IsPrimitive || type.IsValueType || type == typeof(string);
    }
}