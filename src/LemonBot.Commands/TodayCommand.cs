using LemonBot.Clients;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LemonBot.Commands
{
    public class TodayCommand : IBotCommand
    {
        private readonly TwitchClientProxy _client;
        private readonly ILogger<HelpCommand> _logger;

        public string Prefix => "!today";

        public string HelpText => "Show today date";

        public TodayCommand(TwitchClientProxy client, ILogger<HelpCommand> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task ExecuteAsync()
        {
            _client.SendMessage(DateTime.Today.ToShortDateString());
            return Task.CompletedTask;
        }
    }
}
