using System;
using System.Net.Http;
using Newtonsoft.Json;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace moo_telegram.Models.Commands
{
    public class CallbackMoo
    {
        private static InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Му","mooClick")
            }
        });

        private static readonly HttpClient client = new HttpClient();
        public static string Name => @"mooClick";

        public static bool Contains(string callbackQueryData)
        {
            return callbackQueryData == Name;
        }

        public static async void Execute(object sender, CallbackQueryEventArgs ev)
        {
            try
            {
                if (!Contains(ev.CallbackQuery.Data)) return;

                var message = ev.CallbackQuery.Message;
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm")} Received /clickMoo in chat {message.Chat.Id}.");
                
                var response = await client.GetAsync($"https://localhost:44305/Account/ClickMoo?tgId={ev.CallbackQuery.From.Id}");
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonConvert.DeserializeObject<ResponseModel>(responseString);

                Console.WriteLine("Запрос выполнен");

                await Program.botClient.SendTextMessageAsync(message.Chat,
                    $"{responseModel.Message}",
                    replyMarkup: keyboard);
                //await Program.botClient.AnswerCallbackQueryAsync("mooClick");

                await Program.botClient.SendTextMessageAsync("-1001525502746",
                    $"@{ev.CallbackQuery.From.Username}" +
                    $"\n{responseModel.Message}");
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
