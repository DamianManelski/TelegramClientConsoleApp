using FluentArgs;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TelegramClientConsoleApp
{
    public static class Program
    {
        static Task Main(string[] args)
        {

            var appSettings = new TelegramBotSettings();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();

            ConfigurationBinder.Bind(configuration.GetSection("TelegramBotCredentials"), appSettings);

            return FluentArgsBuilder.New()
                .RegisterHelpFlag("-h", "--help", "--another-help-flag")
                .DefaultConfigsWithAppDescription("Telegram console app client.")
                .Parameter("-t", "--text")
                    .WithDescription("message to be send")
                    .WithExamples("simple text")
                    .IsOptional()
                .Call(async (text) =>
                {
                    if (string.IsNullOrEmpty(text))
                    {
                        text = appSettings.Text;
                    }
                    Console.WriteLine($"Sending text: {text} to Telegram chat (id: {appSettings.ChatId}).");

                    var sender = new MessageSender(appSettings.ChatId, appSettings.Token);

                    await sender.SendMessageToTelegramChatAsync(text);
                })
                .ParseAsync(args);
        }
    }
}
