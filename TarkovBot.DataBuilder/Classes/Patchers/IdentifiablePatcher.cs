namespace TarkovBot.DataBuilder.Classes.Patchers;

public class IdentifiablePatcher : IClassPatcher
{
    public void Patch(Class @class)
    {
        if (@class.IsInterface || !string.IsNullOrWhiteSpace(@class.Implementation))
            return;
        if (@class.PropertyDefs.Any(p => p.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase)))
        {
            @class.Implementation = "IIdentifiable";
        }
    }
}