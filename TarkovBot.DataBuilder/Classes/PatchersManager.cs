using TarkovBot.DataBuilder.Classes.Patchers;

namespace TarkovBot.DataBuilder.Classes;

public static class PatchersManager
{
    private static readonly List<IClassPatcher> ClassPatchers = new();

    static PatchersManager()
    {
        ClassPatchers.Add(new IdentifiablePatcher());
    }

    public static void PatchClass(Class @class)
    {
        foreach (IClassPatcher classPatcher in ClassPatchers)
            classPatcher.Patch(@class);
    }
}