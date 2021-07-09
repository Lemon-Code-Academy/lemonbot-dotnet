using System.Threading.Tasks;

namespace LemonBot.Commands
{
    public interface IBotCommand
    {
        string Prefix { get; }

        string HelpText { get; }

        Task ExecuteAsync();
    }
}