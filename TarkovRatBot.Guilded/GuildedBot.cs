// ReSharper disable IdentifierTypo

using System.Collections.ObjectModel;
using Guilded;
using Guilded.Base.Content;
using Guilded.Base.Embeds;
using Guilded.Base.Events;
using Guilded.Base.Users;
using TarkovRatBot.Core.TarkovData;
using TarkovRatBot.Guilded.Extensions;
using static TarkovRatBot.Core.TarkovCore;

namespace TarkovRatBot.Guilded;

public class GuildedBot
{
    public GuildedBot()
    {
        Bot = new GuildedBotClient();
        Bot.Prepared.Subscribe(OnBotReady);
        Bot.MessageCreated.Subscribe(OnMessageReceived);
    }

    public  GuildedBotClient Bot   { get; }

    public async Task Initialize(string token)
    {
        WriteLine("Initializing Guilded Bot...", ConsoleColor.Yellow);
        if (string.IsNullOrWhiteSpace(token))
        {
            WriteLine("Failed to initialize guilded bot. Missing Token.", ConsoleColor.Red);
            return;
        }

        await Bot.ConnectAsync(token);
    }

    private void OnBotReady(Me me)
    {
        WriteLine("Guilded Bot Initialized !", ConsoleColor.Green);
    }

    private async void OnMessageReceived(MessageEvent msg)
    {
        if (msg.Content.StartsWith("?p"))
        {
            string[] split = msg.Content.Split(' ', 2);
            if (split.Length != 2 || string.IsNullOrWhiteSpace(split[1]))
            {
                await msg.ReplyAsync("Wrong Arguments. ?price {item-name}", true);
                return;
            }

            Item[] result = await ItemsByNameQuery.ExecuteAs<Item[]>(split[1]);
            if (result is { Length: 0 })
            {
                await msg.ReplyAsync($"No items found for '{split[1]}'", true);
                return;
            }

            if (result.Length > 1)
            {
                
                return;
            }

            foreach (var embed in result[0].BuildMessageContent())
            {
                if (embed == null)
                {
                    continue;
                }

                await Bot.CreateMessageAsync(msg.ChannelId, false, false, new Guid[]{msg.Message.Id}, embed);
            }
        }
    }
}