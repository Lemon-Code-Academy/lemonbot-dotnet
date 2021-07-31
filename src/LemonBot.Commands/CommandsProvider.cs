using LemonBot.Commands.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LemonBot.Commands
{
    public class CommandsProvider
    {
        public IEnumerable<CommandDescriptor> Commands { get; }

        public CommandsProvider()
        {
            Commands = ResolveAvailableCommands();
        }

        private IEnumerable<CommandDescriptor> ResolveAvailableCommands()
        {
            var botInterfaceType = typeof(IBotCommand);
            var commandTypes = botInterfaceType.Assembly.GetTypes()
                .Where(t => botInterfaceType.IsAssignableFrom(t))
                .Where(t => t.GetCustomAttribute<BotCommandAttribute>() != null)
                .Where(t => t.IsClass)
                .Select(t => new CommandDescriptor
                {
                    CommandType = t,
                    HelpText = t.GetCustomAttribute<BotCommandAttribute>().HelpText,
                    Prefix = t.GetCustomAttribute<BotCommandAttribute>().Prefix
                }).ToArray();

            return commandTypes;
        }
    }
}
