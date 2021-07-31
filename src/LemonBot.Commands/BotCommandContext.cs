namespace LemonBot.Commands
{
    public record BotCommandContext
    {
        public string UserName { get; init; }

        public string Message { get; init; }
    }
}
