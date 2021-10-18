using System;
using System.Net;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace TelegramClientConsoleApp
{
    public class Telegram
    {
        public Telegram()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public async Task<string> SendTelegramAsync(string chatId, string token, string telegramMessage, ParseMode parseMode = ParseMode.Default)
        {
            var reply = string.Empty;

            var id = Convert.ToInt64(chatId);

            try
            {
                var bot = new TelegramBotClient(token);
                await bot.SendTextMessageAsync(id, telegramMessage, parseMode);
                reply = "SUCCESS";
            }
            catch (Exception ex)
            {
                reply = "ERROR: " + ex.Message;
            }

            return reply;
        }
    }
}