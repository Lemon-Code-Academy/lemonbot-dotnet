using LemonBot.Clients;
using LemonBot.Commands.Attributes;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LemonBot.Commands
{
    [BotCommand("!today", HelpText = "Show today date")]
    public class TodayCommand : IBotCommand
    {
        private readonly TwitchClientProxy _client;
        private readonly ILogger<TodayCommand> _logger;

        public TodayCommand(TwitchClientProxy client, ILogger<TodayCommand> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task ExecuteAsync(BotCommandContext context)
        {
            _client.SendMessage(DateTime.Today.ToShortDateString());
            return Task.CompletedTask;
        }
    }
}
