using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using LemonBot.NET.ConsoleApp.Services;
using LemonBot.Clients.Configurations;
using LemonBot.Commands.Extensions;
using LemonBot.Clients.Extensions;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) => {
        var configuration = context.Configuration;

        services.Configure<TwitchBotOptions>(configuration.GetSection("TwitchBot"));

        services.AddTwitchClient();
        services.AddBotCommands();
        services.AddHostedService<TwitchBotService>();
    })
    .Build();

await host.RunAsync();