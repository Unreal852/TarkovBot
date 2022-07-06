using System.Text;
using TarkovBot.DataBuilder.Extensions;

namespace TarkovBot.DataBuilder.Classes;

public class Class : IClass
{
    private readonly List<ClassProperty> _properties = new();

    public Class(string className, string nameSpace)
    {
        ClassName = className.FirstCharToUpperCase();
        Namespace = nameSpace;
    }

    public string ClassName { get; }
    public string Namespace { get; }

    public void AddRawValue(string line)
    {
        if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
            return;
        string[] split = line.TrimStart().Split(' ');
        if (split.Length != 2)
            return;
        string propName = split[0].Replace(":", string.Empty);
        string propType = split[1];
        bool isNullable = !propType.EndsWith('!');
        bool isArray = propType[0] == '[';
        bool isArrayContentNullable = isArray && propType[^(isNullable ? 3 : 2)] != '!';
        propType = propType.Replace("!", string.Empty)
                           .Replace("[", string.Empty)
                           .Replace("]", string.Empty);
        _properties.Add(new ClassProperty(propType, propName, isNullable, isArray, isArrayContentNullable));
    }

    public void AddValue(ClassProperty property)
    {
        _properties.Add(property);
    }

    public void Build(DirectoryInfo outputDirectory)
    {
        if (_properties.Count == 0)
            return;
        Directory.CreateDirectory(outputDirectory.FullName);
        var builder = new StringBuilder();
        builder.Append("using System.Text.Json.Serialization;").AppendLine();                    // Json Attribute using
        builder.Append("using TarkovBot.Core.GraphQL.Attributes;").AppendLine();                 // Custom Attribute Using
        builder.Append("namespace ").Append(Namespace).Append(';').AppendLine();                 // Namespace
        builder.Append("public class ").Append(ClassName).AppendLine().Append('{').AppendLine(); // Class definition
        foreach (ClassProperty classProperty in _properties)
        {
            builder.Append("[JsonPropertyName(\"").Append(classProperty.OriginalName).Append("\")] "); // JsonAttribute
            builder.Append("public ").Append(classProperty.Type);
            if (classProperty.IsArray)
            {
                // if (classProperty.IsArrayContentNullable)
                //     builder.Append('?');
                builder.Append("[]");
            }

            if (classProperty.IsNullable)
                builder.Append('?');
            builder.Append(' ').Append(classProperty.Name).Append(" { get; set; }").AppendLine();
        }

        builder.Append('}'); // End of class

        File.WriteAllText(Path.Combine(outputDirectory.FullName, $"{ClassName}.cs"), builder.ToString());
    }
}