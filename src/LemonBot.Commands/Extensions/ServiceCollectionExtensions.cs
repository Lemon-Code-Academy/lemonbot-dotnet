using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LemonBot.Commands.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBotCommands(this IServiceCollection services)
        {
            services.AddSingleton<BotCommandFactory>();
            services
                .AddSingleton<IBotCommand, HelpCommand>()
                .AddSingleton<IBotCommand, TodayCommand>();

            return services;
        }
    }
}
