using System.Diagnostics;
using TarkovBot.DataBuilder.Classes;
using Enum = TarkovBot.DataBuilder.Classes.Enum;

const string Namespace = "TarkovBot.Core.Data";
const string EndOfParseLine = "# The below types are all deprecated";

args = new[] { Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "schema.graphql") };

if (args is { Length: 0 })
{
    WriteLine("Missing Argument. Please provide a schema '.graphql' file.", ConsoleColor.Red);
    PrintExit();
    return;
}

var fileInfo = new FileInfo(args[0]);
var outputDirectory = new DirectoryInfo(Path.Combine(fileInfo.Directory.FullName, "GeneratedData\\"));

BuildData(outputDirectory, fileInfo);


static void WriteLine(string message, ConsoleColor color = ConsoleColor.White)
{
    Console.ForegroundColor = color;
    Console.WriteLine(message);
    Console.ResetColor();
}

static void PrintExit()
{
    WriteLine("Press any key to exit.", ConsoleColor.Yellow);
    Console.ReadKey();
}

static void BuildData(DirectoryInfo outputDirectory, FileSystemInfo fileInfo)
{
    try
    {
        if (!fileInfo.Exists || !fileInfo.Extension.EndsWith("graphql"))
        {
            WriteLine("The specified file does not exists or is not a .graphql file.");
            PrintExit();
            return;
        }

        Directory.CreateDirectory(outputDirectory.FullName);
        foreach (FileInfo file in outputDirectory.EnumerateFiles())
            file.Delete();

        Stopwatch sw = Stopwatch.StartNew();
        string[] lines = File.ReadAllLines(fileInfo.FullName);
        int totalClasses = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            bool isClass = line.StartsWith("type") || line.StartsWith("interface");
            bool isEnum = !isClass && line.StartsWith("enum");
            if (!isClass && !isEnum)
                continue;
            string[] classDefinition = line.Split(' ');
            IClass @class = isClass ? new Class(classDefinition[1], Namespace) : new Enum(classDefinition[1], Namespace);
            WriteLine($"Building class '{@class.ClassName}'...", ConsoleColor.Yellow);
            var classLine = string.Empty;
            while (!(classLine = lines[++i]).StartsWith('}'))
            {
                if (classLine.Contains("@deprecated") || i + 1 < lines.Length && lines[i + 1].Contains("@deprecated"))
                    continue;
                @class.AddRawValue(classLine);
            }

            @class.Build(outputDirectory);
            totalClasses++;
        }

        {
            WriteLine("Building class 'IdOnly'...", ConsoleColor.Yellow);
            var idOnlyClass = new Class("IdOnly", Namespace);
            idOnlyClass.AddValue(new ClassProperty("string", "id", false, false, false));
            idOnlyClass.Build(outputDirectory);
            totalClasses++;
        }

        sw.Stop();

        WriteLine($"Successfully built {totalClasses} classes in {sw.Elapsed.TotalMilliseconds:F}ms", ConsoleColor.Green);
        PrintExit();
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        Console.ReadKey();
    }
}