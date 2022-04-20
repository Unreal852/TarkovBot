using System.Reflection;

namespace TarkovRatBot.Extensions;

public static class AssemblyExtensions
{
    public static string ReadFileToEnd(this Assembly assembly, string fileName)
    {
        Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Resources.{fileName}");
        if (stream == null)
            return string.Empty;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }
}