using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LemonBot.Commands
{
    public class BotCommandFactory
    {
        private readonly IServiceProvider _provider;

        public IDictionary<string, IBotCommand> CommandsMap { get; }

        public BotCommandFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            CommandsMap = new Dictionary<string, IBotCommand>();
            ResolveBotCommands();
        }

        public IBotCommand GetCommandByPrefix(string commandPrefix)
        {
            if (string.IsNullOrWhiteSpace(commandPrefix))
            {
                throw new ArgumentException("value cannot be empty", nameof(commandPrefix));
            }

            if (!CommandsMap.ContainsKey(commandPrefix))
            {
                return null;
            }

            return CommandsMap[commandPrefix];
        }

        private void ResolveBotCommands()
        {
            var commands = _provider.GetServices<IBotCommand>();
            if (commands?.Any() ?? false)
            {
                foreach (var command in commands)
                {
                    if (!CommandsMap.ContainsKey(command.Prefix))
                    {
                        CommandsMap.Add(command.Prefix, command);
                    }
                }
            }
        }
    }
}
