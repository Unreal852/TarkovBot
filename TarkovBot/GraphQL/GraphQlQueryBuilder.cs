using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using SerilogTimings;
using TarkovBot.Core.GraphQL.Attributes;
using TarkovBot.Extensions;

namespace TarkovBot.GraphQL;

public static class GraphQlQueryBuilder
{
    private static readonly BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty;

    public static GraphQlQuery? FromType<T>()
    {
        Type type = typeof(T);

        GraphQlQuery? graphQlQuery;
        using (Operation.Time("Built GraphQL query from type '{Type}'", type.Name))
        {
            if (!type.IsClass || type.IsAbstract)
                return null;
            var graphQlAttribute = type.GetCustomAttribute<GraphQlAttribute>();
            if (graphQlAttribute == null)
                return null;
            StringBuilder builder = new StringBuilder("{").AppendLine();
            builder.Append(graphQlAttribute.Name).Append("(${ARGS})").AppendLine();

            BuildObject(type, builder);
            builder.Append('}');
            graphQlQuery = new GraphQlQuery(graphQlAttribute.Name, builder.ToString());
        }

        return graphQlQuery;
    }

    public static GraphQlQuery? FromResource(string name)
    {
        using (Operation.Time("Built GraphQL query from resource '{Name}'", name))
        {
            var assembly = Assembly.GetExecutingAssembly();
            using Stream? stream = assembly.GetManifestResourceStream($"TarkovBot.Resources.{name}{(name.EndsWith(".ql") ? "" : ".ql")}");
            if (stream == null)
                return default;
            using var reader = new StreamReader(stream);
            return new GraphQlQuery(name, reader.ReadToEnd());
        }
    }

    private static void BuildObject(Type type, StringBuilder builder)
    {
        builder.Append('{').AppendLine();
        foreach (PropertyInfo property in GetProperties(type))
        {
            if (property.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                continue;
            string propName = property.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name ?? property.Name;
            propName = propName.FirstCharToLowerCase();
            if (IsPrimitive(property.PropertyType))
                builder.Append(propName).Append(',').AppendLine();
            else if (property.PropertyType.IsArray)
            {
                if (property.PropertyType.GetElementType()!.IsEnum)
                    builder.Append(propName).Append(',').AppendLine();
                else
                {
                    builder.Append(propName);
                    BuildObject(property.PropertyType.GetElementType()!, builder);
                }
            }
            else
            {
                builder.Append(propName);
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