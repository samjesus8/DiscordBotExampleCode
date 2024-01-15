using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DiscordBotTutorialExampleProject.Commands.Slash
{
    [SlashCommandGroup("calculator", "Perform calculator operations")]
    public class CalculatorSL : ApplicationCommandModule
    {
        [SlashCommand("add", "Add 2 numbers together")]
        public async Task Add(InteractionContext ctx, [Option("number1", "Number 1")] double number1, [Option("number2", "Number 2")] double number2)
        {
            await ctx.DeferAsync();

            var outputEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = $"{number1} + {number2}",
                Description = $"{number1 + number2}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }

        [SlashCommand("subtract", "Subtract 2 numbers")]
        public async Task Subtract(InteractionContext ctx, [Option("number1", "Number 1")] double number1, [Option("number2", "Number 2")] double number2)
        {
            await ctx.DeferAsync();

            var outputEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = $"{number1} - {number2}",
                Description = $"{number1 - number2}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }

        [SlashCommand("multiply", "Multiply 2 numbers together")]
        public async Task Multiply(InteractionContext ctx, [Option("number1", "Number 1")] double number1, [Option("number2", "Number 2")] double number2)
        {
            await ctx.DeferAsync();

            var outputEmbed = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Green,
                Title = $"{number1} x {number2}",
                Description = $"{number1 * number2}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(outputEmbed));
        }
    }
}
