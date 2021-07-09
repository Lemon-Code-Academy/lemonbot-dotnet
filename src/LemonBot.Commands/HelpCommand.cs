using LemonBot.Clients;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LemonBot.Commands
{
    public class HelpCommand : IBotCommand
    {
        private readonly TwitchClientProxy _client;
        private readonly ILogger<HelpCommand> _logger;

        public string Prefix { get; } = "!help";

        public string HelpText { get; } = "Show some help for BOT commands";

        public HelpCommand(TwitchClientProxy client, ILogger<HelpCommand> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task ExecuteAsync()
        {
            string helpText = CreateHelpText();
            _client.SendMessage(helpText);

            return Task.CompletedTask;
        }

        private string CreateHelpText()
        {
            return "HELP";
        }
    }
}