using TarkovBot.DataBuilder.Extensions;

namespace TarkovBot.DataBuilder.Classes;

public class ClassProperty
{
    public ClassProperty(string type, string name, bool isNullable, bool isArray, bool isArrayContentNullable)
    {
        Type = AdaptType(type);
        Name = name.FirstCharToUpperCase();
        OriginalName = name;
        IsNullable = isNullable;
        IsArray = isArray;
        IsArrayContentNullable = isArrayContentNullable;
    }

    public string Type                   { get; }
    public string Name                   { get; }
    public string OriginalName           { get; }
    public bool   IsNullable             { get; }
    public bool   IsArray                { get; }
    public bool   IsArrayContentNullable { get; }

    private static string AdaptType(string type)
    {
        return type switch
        {
                "ID" or "String" or "ItemProperties" => "string",
                "Float"                              => "float",
                "Double"                             => "double",
                "Int"                                => "int",
                "Boolean"                            => "bool",
                _                                    => type
        };
    }
}