using DiscordBotTutorialExampleProject.Commands.Prefix;
using DiscordBotTutorialExampleProject.Commands.Slash;
using DiscordBotTutorialExampleProject.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;

namespace DiscordBotTutorialExampleProject
{
    //Make sure the Program class is sealed so that nothing can inherit it
    public sealed class Program
    {
        public static DiscordClient Client { get; set; }
        public static CommandsNextExtension Commands { get; set; }
        static async Task Main(string[] args)
        {
            //1. Retrieve Token/Prefix from config.json
            var configProperties = new ConfigJsonReader();
            await configProperties.ReadJSON();

            //2. Create the Bot Configuration
            var discordConfig = new DiscordConfiguration
            {
                Intents = DiscordIntents.All,
                Token = configProperties.discordToken,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
            };

            //3. Initialise the DiscordClient property
            Client = new DiscordClient(discordConfig);

            //Set defaults for interactivity based events
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            //4. Set up the Ready Event-Handler
            //Event-Handlers for the Client go here
            Client.Ready += Client_Ready;
            Client.GuildMemberAdded += Client_GuildMemberAdded;
            Client.ComponentInteractionCreated += Client_ComponentInteractionCreated;
            Client.ModalSubmitted += Client_ModalSubmitted;

            //5. Create the Command Configuration
            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configProperties.discordPrefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            //6. Initialize the CommandsNextExtention property
            Commands = Client.UseCommandsNext(commandsConfig);

            //Enabling the Client to use Slash Commands
            var slashCommandsConfig = Client.UseSlashCommands();

            //7. Register your Command Classes
            Commands.RegisterCommands<Basics>();
            Commands.RegisterCommands<DiscordComponentExamples>();

            //Registering Slash Commands
            slashCommandsConfig.RegisterCommands<BasicSL>();
            slashCommandsConfig.RegisterCommands<CalculatorSL>();

            //8. Connect the Client to the Discord Gateway
            await Client.ConnectAsync();

            //Make sure you delay by -1 to keep the bot running forever
            await Task.Delay(-1);
        }

        private static async Task Client_ModalSubmitted(DiscordClient sender, ModalSubmitEventArgs args)
        {
            if (args.Interaction.Type == InteractionType.ModalSubmit && args.Interaction.Data.CustomId == "testModal")
            {
                var values = args.Values;
                await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.Interaction.User.Username} submitted a modal with the input {values.Values.First()}"));
            }
        }

        private static async Task Client_ComponentInteractionCreated(DiscordClient sender, ComponentInteractionCreateEventArgs args)
        {
            switch (args.Interaction.Data.CustomId)
            {
                case "button1":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 1"));
                    break;
                case "button2":
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has pressed button 2"));
                    break;
                case "basicsButton":
                    var basicCommandsEmbed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Black,
                        Title = "Basic Commands",
                        Description = "- !test -> Send a basic message \n" +
                                      "- !embed -> Sends a basic embed message \n" +
                                      "- !calculator -> Performs an operation on 2 numbers \n" +
                                      "- !cardgame -> Play a simple card game. Highest number wins the game"
                    };

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(basicCommandsEmbed));
                    break;
                case "calculatorButton":
                    var calculatorCommandsEmbed = new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.Black,
                        Title = "Basic Commands",
                        Description = "- /calculator add -> Adds 2 numbers together \n" +
                                      "- /calculator subtract -> Subtracts 2 numbers \n" +
                                      "- /calculator multiply -> Multiplies 2 numbers together"
                    };

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().AddEmbed(calculatorCommandsEmbed));
                    break;
                case "modalButton":
                    var modal = new DiscordInteractionResponseBuilder()
                        .WithTitle("Test Modal")
                        .WithCustomId("testModal")
                        .AddComponents(new TextInputComponent("Random", "randomTextBox", "Type something here"));

                    await args.Interaction.CreateResponseAsync(InteractionResponseType.Modal, modal);
                    break;
            }

            //Drop-Down Events (You can change this to a SWITCH block!!!!)
            if (args.Id == "dropDownList" && args.Interaction.Data.ComponentType == ComponentType.StringSelect)
            {
                var options = args.Values;
                foreach (var option in options)
                {
                    switch (option)
                    {
                        case "option1":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has selected Option 1"));
                            break;

                        case "option2":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has selected Option 2"));
                            break;

                        case "option3":
                            await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} has selected Option 3"));
                            break;
                    }
                }
            }
            else if (args.Id == "channelDropDownList")
            {
                var options = args.Values;
                foreach (var channel in options)
                {
                    var selectedChannel = await Client.GetChannelAsync(ulong.Parse(channel));
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"{args.User.Username} selected the channel with name {selectedChannel.Name}"));
                }
            }

            else if (args.Id == "mentionDropDownList")
            {
                var options = args.Values;
                foreach (var user in options)
                {
                    var selectedUser = await Client.GetUserAsync(ulong.Parse(user));
                    await args.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent($"{selectedUser.Mention} was mentionned"));
                }
            }
        }

        private static async Task Client_GuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs args)
        {
            var defaultChannel = args.Guild.GetDefaultChannel();

            var welcomeEmbed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Gold,
                Title = $"Welcome {args.Member.Username} to the server",
                Description = "Hope you enjoy your stay, please read the rules"
            };

            await defaultChannel.SendMessageAsync(embed: welcomeEmbed);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
