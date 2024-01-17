using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DiscordBotTutorialExampleProject.Commands.Prefix
{
    public class DiscordComponentExamples : BaseCommandModule
    {
        //This class shows how to make implement different DiscordComponent types

        [Command("button")]
        public async Task Buttons(CommandContext ctx)
        {
            //Declare your buttons before doing anything else
            var button = new DiscordButtonComponent(ButtonStyle.Primary, "button1", "Button 1");
            var button2 = new DiscordButtonComponent(ButtonStyle.Primary, "button2", "Button 2");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Aquamarine)
                    .WithTitle("Test Embed"))
                .AddComponents(button, button2);

            //A Message can have up to 5 x 5 worth of buttons. Thats 5 rows, each with 5 buttons, 25 buttons in total

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

        [Command("dropdown-list")]
        public async Task DropdownList(CommandContext ctx)
        {
            //Declare the list of options in the drop-down
            List<DiscordSelectComponentOption> optionList = new List<DiscordSelectComponentOption>();
            optionList.Add(new DiscordSelectComponentOption("Option 1", "option1"));
            optionList.Add(new DiscordSelectComponentOption("Option 2", "option2"));
            optionList.Add(new DiscordSelectComponentOption("Option 3", "option3"));

            //Turn the list into an IEnumerable for the Component
            var options = optionList.AsEnumerable();

            //Make the drop-down component
            var dropDown = new DiscordSelectComponent("dropDownList", "Select...", options);

            //Make and send off the message with the component
            var dropDownMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Gold)
                    .WithTitle("This embed has a drop-down list on it"))
                .AddComponents(dropDown);

            await ctx.Channel.SendMessageAsync(dropDownMessage);
        }

        [Command("channel-list")]
        public async Task ChannelList(CommandContext ctx)
        {
            var channelComponent = new DiscordChannelSelectComponent("channelDropDownList", "Select...");

            var dropDownMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Gold)
                    .WithTitle("This embed has a channel drop-down list on it"))
                .AddComponents(channelComponent);

            await ctx.Channel.SendMessageAsync(dropDownMessage);
        }

        [Command("mention-list")]
        public async Task MentionList(CommandContext ctx)
        {
            var mentionComponent = new DiscordMentionableSelectComponent("mentionDropDownList", "Select...");

            var dropDownMessage = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Gold)
                    .WithTitle("This embed has a mention drop-down list on it"))
                .AddComponents(mentionComponent);

            await ctx.Channel.SendMessageAsync(dropDownMessage);
        }

        [Command("modal")]
        public async Task Modal(CommandContext ctx)
        {
            var modalButton = new DiscordButtonComponent(ButtonStyle.Primary, "modalButton", "Modal");

            var message = new DiscordMessageBuilder()
                .AddEmbed(new DiscordEmbedBuilder()
                    .WithColor(DiscordColor.Azure)
                    .WithTitle("Modal Example")
                    .WithDescription("Press the button to open up a modal!!!"))
                .AddComponents(modalButton);

            await ctx.Channel.SendMessageAsync(message);
        }
    }
}
