using DiscordBotTutorialExampleProject.Commands.Prefix;
using DiscordBotTutorialExampleProject.Config;
using DSharpPlus;
using DSharpPlus.CommandsNext;
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
            Client.Ready += Client_Ready;

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

            //8. Connect the Client to the Discord Gateway
            await Client.ConnectAsync();
            //Make sure you delay by -1 to keep the bot running forever
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}
