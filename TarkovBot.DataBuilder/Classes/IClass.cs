namespace TarkovBot.DataBuilder.Classes;

public interface IClass
{
    public string                ClassName { get; }
    public string                Namespace { get; }
    public IEnumerable<UsingDef> UsingDefs { get; }

    public void AddRawProperty(string line);
    public void AddRawUsing(string line);

    public void Build(DirectoryInfo outputDirectory);
}