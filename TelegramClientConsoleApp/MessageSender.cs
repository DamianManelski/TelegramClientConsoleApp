using System;
using System.Threading.Tasks;
using Telegram.Bot.Types.Enums;

namespace TelegramClientConsoleApp
{
    public class MessageSender
    {
        private readonly Telegram _telegramClient;

        public string ChatId { get; set; }

        public string Token { get; set; }

        public MessageSender(string chatId, string token)
        {
            ChatId = chatId ?? throw new ArgumentNullException(nameof(chatId));
            Token = token ?? throw new ArgumentNullException(nameof(token));
            _telegramClient = new Telegram();
        }

        public async Task SendMessageToTelegramChatAsync(string message)
        {
            var errorMessage = await _telegramClient.SendTelegramAsync(ChatId, Token, message, ParseMode.Default);
            if (errorMessage != "SUCCESS")
            {
                Console.WriteLine($"Cannot send message. Error: {errorMessage}");
            }
            else
            {
                Console.WriteLine("Message successfully sended.");
            }
        }
    }
}
