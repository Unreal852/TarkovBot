using Guilded.Base.Embeds;

namespace TarkovBot.Embeds;

public class HelpEmbed : Embed
{
    public HelpEmbed(string? description = null)
    {
        Title = "EFT - Help";
        Description = description;
        Color = System.Drawing.Color.Cyan;

        AddField("`t!h`",
                "Show the help menu (this message)");

        AddField("`t!i [item name]`",
                "Search for an item by its name" +
                "\nExample: `t!i afak`");
    }
}