using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LemonBot.Commands
{
    public class BotCommandResolver
    {
        private readonly IServiceProvider _provider;

        private readonly CommandsProvider _commandsProvider;

        public IDictionary<string, IBotCommand> CommandsMap { get; }

        public BotCommandResolver(IServiceProvider provider, CommandsProvider commandsProvider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
            _commandsProvider = commandsProvider ?? throw new ArgumentNullException(nameof(commandsProvider));

            //CommandsMap = new Dictionary<string, IBotCommand>();
            //ResolveBotCommands();
        }

        public IBotCommand ResolveByMessage(string message)
        {
            foreach (var commandDescriptor in _commandsProvider.Commands)
            {
                if (message.Contains(commandDescriptor.Prefix, StringComparison.InvariantCultureIgnoreCase))
                {
                    var command = _provider.GetRequiredService(commandDescriptor.CommandType) as IBotCommand;
                    return command;
                }
            }

            return null;
        }

        //public IBotCommand GetCommandByPrefix(string commandPrefix)
        //{
        //    if (string.IsNullOrWhiteSpace(commandPrefix))
        //    {
        //        throw new ArgumentException("value cannot be empty", nameof(commandPrefix));
        //    }

        //    if (!CommandsMap.ContainsKey(commandPrefix))
        //    {
        //        return null;
        //    }

        //    return CommandsMap[commandPrefix];
        //}

        //private void ResolveBotCommands()
        //{
        //    var commands = _provider.GetServices<IBotCommand>();
        //    if (commands?.Any() ?? false)
        //    {
        //        foreach (var command in commands)
        //        {
        //            if (!CommandsMap.ContainsKey(command.Prefix))
        //            {
        //                CommandsMap.Add(command.Prefix, command);
        //            }
        //        }
        //    }
        //}
    }
}
