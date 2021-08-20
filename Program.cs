using System;
using moo_telegram.Models;
using moo_telegram.Models.Commands;
using Telegram.Bot;

namespace moo_telegram
{
    class Program
    {
        public static ITelegramBotClient botClient;
        static void Main(string[] args)
        {
            botClient = new TelegramBotClient(AppSettings.Key);

            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
            );

            botClient.OnMessage += StartCommand.Execute;
            botClient.OnCallbackQuery += CallbackMoo.Execute;

            botClient.StartReceiving();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            botClient.StopReceiving();
        }
    }
}
