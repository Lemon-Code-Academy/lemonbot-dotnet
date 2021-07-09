using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TwitchLib.Client.Events;
using Microsoft.Extensions.Logging;
using LemonBot.Clients;
using LemonBot.Commands;

namespace LemonBot.NET.ConsoleApp.Services
{
    public class TwitchBotService : BackgroundService 
    {
        private readonly ILogger<TwitchBotService> _logger;

        private readonly TwitchClientProxy _client;

        private readonly BotCommandFactory _commandFactory;

        private HashSet<string> _usersAlreadyJoined;

        public TwitchBotService(TwitchClientProxy client, BotCommandFactory commandFactory, ILogger<TwitchBotService> logger)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _commandFactory = commandFactory ?? throw new ArgumentNullException(nameof(commandFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _usersAlreadyJoined = new();
        }

        protected override  Task ExecuteAsync(CancellationToken stoppingToken)
        {
            InitializeTwitchClient();
            ConnectToTwitch();

            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            UnregisterClientEvents();
            return base.StopAsync(cancellationToken);
        }

        private void InitializeTwitchClient()
        {
            _client.Initialize();
            RegisterClientEvents();
        }

        private void UnregisterClientEvents()
        {
            _client.RemoveClientEvents(c =>
            {
                c.OnLog -= OnClientLog;
                c.OnConnected -= OnClientConnected;
                c.OnConnectionError -= OnConnectionErrorOccured;
                c.OnChatCommandReceived -= OnChatCommandReceived;
            });
        }

        private void RegisterClientEvents()
        {
            _client.ConfigureClientEvents(c =>
            {
                c.OnLog += OnClientLog;
                c.OnConnected += OnClientConnected;
                c.OnConnectionError += OnConnectionErrorOccured;
                c.OnChatCommandReceived += OnChatCommandReceived;
            });
        }

        private async void OnChatCommandReceived(object sender, OnChatCommandReceivedArgs e)
        {
            var command = _commandFactory.GetCommandByPrefix(e.Command.ChatMessage.Message);
            await command?.ExecuteAsync();
        }

        private void OnConnectionErrorOccured(object sender, OnConnectionErrorArgs e)
        {
            _logger.LogWarning("Error connecting {BotUsername}: {Error}", e.BotUsername, e.Error);
        }

        private void OnClientConnected(object sender, OnConnectedArgs e)
        {
            _logger.LogInformation("{BotUsername} connected successfully", e.BotUsername);

            _client.Join();
            _client.SendMessage($"Hi everyone, I'm {e.BotUsername}");
        }

        private void OnClientLog(object sender, OnLogArgs e)
        {
            _logger.LogInformation("[{LogDate}] Bot: {BotUsername}, Data: {Data}", e.DateTime, e.BotUsername, e.Data);
        }

        private void ConnectToTwitch() => _client.Connect();
    }
}