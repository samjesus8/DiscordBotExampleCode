using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace DiscordBotTutorialExampleProject.Commands.Slash
{
    public class BasicSL : ApplicationCommandModule
    {
        [SlashCommand("test", "This is my first slash command")]
        public async Task MyFirstSlashCommand(InteractionContext ctx)
        {
            await ctx.DeferAsync();

            var embedMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Blue,
                Title = "Test Embed"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
        }

        [SlashCommand("parameters", "This slash command allows parameters")]
        public async Task SlashCommandParameters(InteractionContext ctx, [Option("testoption", "Type in anything")] string testParameter, [Option("numberoption", "Type in a number")] long number)
        {
            await ctx.DeferAsync();

            var embedMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Brown,
                Title = "Test Embed",
                Description = $"{testParameter} {number}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
        }

        [SlashCommand("discordParameters", "This slash command allows passing of DiscordParameters")]
        public async Task DiscordParameters(InteractionContext ctx, [Option("user", "Pass in a Discord User")] DiscordUser user, [Option("file", "Upload a file here")] DiscordAttachment file)
        {
            await ctx.DeferAsync();

            var member = (DiscordMember) user;

            var embedMessage = new DiscordEmbedBuilder
            {
                Color = DiscordColor.Blue,
                Title = "Test Embed",
                Description = $"{member.Nickname} {file.FileName} {file.FileSize}"
            };

            await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(embedMessage));
        }
    }
}
