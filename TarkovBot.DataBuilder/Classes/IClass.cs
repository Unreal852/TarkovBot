namespace TarkovBot.DataBuilder.Classes;

public interface IClass
{
    public string ClassName { get; }
    public string Namespace { get; }

    public void AddRawValue(string line);

    public void Build(DirectoryInfo outputDirectory);
}