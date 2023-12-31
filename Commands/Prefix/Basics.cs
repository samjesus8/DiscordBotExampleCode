using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DiscordBotTutorialExampleProject.Commands.Prefix
{
    //Every Command class must be PUBLIC and must inherit BaseCommandModule
    public class Basics : BaseCommandModule
    {
        //Use the Command attribute to declare a command
        [Command("test")]

        //Then create an async method for the command. It MUST have the CommandContext as its 1st parameter
        public async Task MyFirstCommand(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hello World!");
        }

        [Command("embed")]
        public async Task EmbedMessageExamples(CommandContext ctx)
        {
            //Using a DiscordMessageBuilder
            var embedMessage1 = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Embed Message Title")
                    .WithDescription("Embed Message Description"));

            //Using a DiscordEmbedBuilder
            var embedMessage2 = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = "Embed Message Title",
                Description = "Embed Message Description"
            };

            await ctx.Channel.SendMessageAsync(embedMessage1);
            await ctx.Channel.SendMessageAsync(embed: embedMessage2);
        }

        [Command("calculator")]
        public async Task CommandParametersExample(CommandContext ctx, int num1, string operation, int num2)
        {
            int result = 0;

            switch(operation)
            {
                case "+":
                    result = num1 + num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "/":
                    result = num1 / num2;
                    break;
                default:
                    await ctx.Channel.SendMessageAsync("Please enter a valid operation (+, -, *, /)");
                    break;
            }

            await ctx.Channel.SendMessageAsync(result.ToString());
        }
    }
}
