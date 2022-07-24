using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Text;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Commands;

namespace TarkovBot.Guilded.Commands;

[SuppressMessage("Performance", "CA1806:Ne pas ignorer les résultats des méthodes")]
public class HelpCommand : CommandModule
{
    public HelpCommand(GuildedBot botClient)
    {
        BotClient = botClient;
    }

    private GuildedBot     BotClient { get; }
    private MessageContent Message   { get; init; }

    [Command("help", Aliases = new[] { "h" })]
    public Task HelpCommandAsync(CommandEvent commandEvent)
    {
        var messageContent = new MessageContent
        {
                Embeds = new Collection<Embed>()
        };

        var builder = new StringBuilder();
        builder.AppendLine("**Commands**");
        builder.AppendLine("`t!h` - Show this help message");
        builder.AppendLine("`t!i <item>` - Search for an item");
        builder.AppendLine("`t!i <lang> <item>` - Search for an item in a specific language (en, fr)");

        var embed = new Embed
        {
                Title = "Tarkov Bot Help",
                Color = Color.GreenYellow,
                Description = builder.ToString()
        };

        return commandEvent.ReplyAsync(true, false, embed);
    }
}