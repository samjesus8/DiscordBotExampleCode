using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DiscordBotTutorialExampleProject.Commands.Prefix
{
    public class InteractionComponents : BaseCommandModule
    {
        [Command("button")]
        public async Task Buttons(CommandContext ctx)
        {
            var button = new DiscordButtonComponent(ButtonStyle.Primary, "button1", "Button 1");
            var button2 = new DiscordButtonComponent(ButtonStyle.Primary, "button2", "Button 2");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Aquamarine)
                    .WithTitle("Test Embed"))
                .AddComponents(button, button2);

            await ctx.Channel.SendMessageAsync(message);
        }

        [Command("help")]
        public async Task HelpCommand(CommandContext ctx)
        {
            var basicsButton = new DiscordButtonComponent(ButtonStyle.Primary, "basicsButton", "Basics");
            var calculatorButton = new DiscordButtonComponent(ButtonStyle.Success, "calculatorButton", "Calculator");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Black)
                    .WithTitle("Help Section")
                    .WithDescription("Please press a button to view its commands"))
                .AddComponents(basicsButton, calculatorButton);

            await ctx.Channel.SendMessageAsync(message);
        }
    }
}
