using DiscordBotTutorialExampleProject.Engine.CardGame;
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

        [Command("cardgame")]
        public async Task CardGameUsingEmbed(CommandContext ctx)
        {
            //Creating an instance of a card for the user
            var UserCard = new CardBuilder();

            //Displaying the User's card in an embed
            var userCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Your Card")
                    .WithDescription("You drew a: " + UserCard.SelectedCard));

            await ctx.Channel.SendMessageAsync(userCardMessage);

            //Creating an instance of a card for the Bot
            var BotCard = new CardBuilder();

            //Displaying the Bot's card in an embed
            var botCardMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Bot Card")
                    .WithDescription("The Bot drew a: " + BotCard.SelectedCard));

            await ctx.Channel.SendMessageAsync(botCardMessage);

            //Comparing the two cards
            if (UserCard.SelectedNumber > BotCard.SelectedNumber)
            {
                //The User wins
                var winningMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Win the game!!**",
                    Color = DiscordColor.Green
                };

                await ctx.Channel.SendMessageAsync(embed: winningMessage);
            }
            else
            {
                //The Bot wins
                var losingMessage = new DiscordEmbedBuilder()
                {
                    Title = "**You Lost the game**",
                    Color = DiscordColor.Red
                };

                await ctx.Channel.SendMessageAsync(embed: losingMessage);
            }
        }

        [Command("poll")]
        public async Task BasicPollExample(CommandContext ctx, string option1, string option2, string option3, string option4, [RemainingText] string pollTitle)
        {
            DiscordEmoji[] emojiOptions = [ DiscordEmoji.FromName(Program.Client, ":one:"),
                                            DiscordEmoji.FromName(Program.Client, ":two:"),
                                            DiscordEmoji.FromName(Program.Client, ":three:"),
                                            DiscordEmoji.FromName(Program.Client, ":four:") ];

            string optionsDescription = $"{emojiOptions[0]} | **{option1}** \n" +
                                        $"{emojiOptions[1]} | **{option2}** \n" +
                                        $"{emojiOptions[2]} | **{option3}** \n" +
                                        $"{emojiOptions[3]} | **{option4}**";

            var pollMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Azure,
                Title = pollTitle,
                Description = optionsDescription
            };

            var message = await ctx.Channel.SendMessageAsync(embed: pollMessage);
            foreach (var emoji in emojiOptions)
            {
                await message.CreateReactionAsync(emoji);
            }
        }
    }
}
