using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace moo_telegram.Models.Commands
{
    public class StartCommand
    {
        private static InlineKeyboardMarkup keyboard = new InlineKeyboardMarkup(new[]
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("Му","mooClick")
            }
        });

        private static readonly HttpClient client = new HttpClient();

        public static string Name => @"/start";

        public static bool Contains(Message message)
        {
            return message.Type == Telegram.Bot.Types.Enums.MessageType.Text && message.Text == Name;
        }

        public static async void Execute(object sender, MessageEventArgs e)
        {
            try
            {
                if (!Contains(e.Message)) return;

                var message = e.Message;
                Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm")} Received /start in chat {message.Chat.Id}.");

                var userModel = new UserModel()
                {
                    TgId = message.From.Id,
                    TgUsername = message.From.Username,
                    TgFirstName = message.From.FirstName,
                    TgLastName = message.From.LastName,
                    TgLanguageCode = message.From.LanguageCode
                };

                var requestContent = new StringContent(JsonConvert.SerializeObject(userModel), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44305/Account/Start", requestContent);
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                userModel = JsonConvert.DeserializeObject<UserModel>(responseString);

                Console.WriteLine("Запрос выполнен");

                await Program.botClient.SendTextMessageAsync(message.Chat,
                    $"Следи за всеми коровами в загоне: https://t.me/joinchat/RVNFkM0qjNZjMTMy" +
                    $"\nТекущее муу: {userModel.MooCount}",
                    replyMarkup: keyboard);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
