using System.Text;

namespace TarkovBot.DataBuilder.Classes;

public class Enum : IClass
{
    private readonly List<string> _values = new();

    public Enum(string className, string nameSpace)
    {
        ClassName = className;
        Namespace = nameSpace;
    }

    public string ClassName { get; }
    public string Namespace { get; }

    public void AddRawValue(string line)
    {
        if (string.IsNullOrWhiteSpace(line) || line.TrimStart().StartsWith("#"))
            return;
        _values.Add(line.Trim());
    }

    public void Build(DirectoryInfo outputDirectory)
    {
        if (_values.Count == 0)
            return;
        Directory.CreateDirectory(outputDirectory.FullName);
        var builder = new StringBuilder();
        builder.Append("using System.Text.Json.Serialization;").AppendLine();                   // Json Attribute using
        builder.Append("using TarkovBot.Core.GraphQL.Attributes;").AppendLine();                // Custom Attribute Using
        builder.Append("namespace ").Append(Namespace).Append(';').AppendLine();                // Namespace
        builder.Append("public enum ").Append(ClassName).AppendLine().Append('{').AppendLine(); // Class definition
        foreach (string enumValue in _values)
        {
            builder.Append("[JsonPropertyName(\"").Append(enumValue).Append("\")] "); // JsonAttribute
            builder.Append(enumValue).Append(',').AppendLine();
        }

        builder.Append('}'); // End of class
        File.WriteAllText(Path.Combine(outputDirectory.FullName, $"{ClassName}.cs"), builder.ToString());
    }
}