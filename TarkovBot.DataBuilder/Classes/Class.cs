using System.Text;
using TarkovBot.DataBuilder.Extensions;

namespace TarkovBot.DataBuilder.Classes;

public class Class : IClass
{
    private readonly List<UsingDef>    _usings     = new();
    private readonly List<PropertyDef> _properties = new();

    public Class(string className, string nameSpace, bool isInterface = false)
    {
        ClassName = className.FirstCharToUpperCase();
        Namespace = nameSpace;
        IsInterface = isInterface;

        AddRawUsing("using System.Text.Json.Serialization;");    // Json Attribute using
        AddRawUsing("using TarkovBot.Core.GraphQL.Attributes;"); // Custom Attribute Using
    }

    public string                   ClassName      { get; set; }
    public string                   Namespace      { get; set; }
    public string                   Implementation { get; set; }
    public bool                     IsInterface    { get; set; }
    public IEnumerable<UsingDef>    UsingDefs      => _usings;
    public IEnumerable<PropertyDef> PropertyDefs   => _properties;

    public void AddRawProperty(string line)
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
        _properties.Add(new PropertyDef(propType, propName, isNullable, isArray, isArrayContentNullable));
    }

    public void AddProperty(PropertyDef propertyDef)
    {
        _properties.Add(propertyDef);
    }

    public void AddRawUsing(string line)
    {
        if (!line.StartsWith("using"))
            line = $"using {line}";
        if (!line.EndsWith(';'))
            line += ';';
        _usings.Add(new UsingDef(line));
    }

    public void AddUsing(UsingDef usingDef)
    {
        _usings.Add(usingDef);
    }

    public void Build(DirectoryInfo outputDirectory)
    {
        PatchersManager.PatchClass(this);

        if (_properties.Count == 0)
            return;

        Directory.CreateDirectory(outputDirectory.FullName);
        var builder = new StringBuilder();

        foreach (UsingDef usingDef in _usings)
            builder.Append(usingDef.Using).AppendLine();

        builder.Append("namespace ").Append(Namespace).Append(';').AppendLine();              // Namespace
        builder.Append(IsInterface ? "public interface " : "public class").Append(ClassName); // Class Definition
        if (!string.IsNullOrWhiteSpace(Implementation))
            builder.Append(" : ").Append(Implementation);
        builder.AppendLine().Append('{').AppendLine(); // Class definition
        foreach (PropertyDef classProperty in _properties)
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