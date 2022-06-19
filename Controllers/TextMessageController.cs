using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_11_6.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Task_11_6.Controllers
{
    public class TextMessageController
    {
        private readonly ITelegramBotClient _telegramClient;
        private readonly IStorage _memoryStorage;
        private readonly IProcess _processInputString;

        public TextMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage, IProcess processInputString)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
            _processInputString = processInputString;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var session = _memoryStorage.GetSession(message.Chat.Id);

            switch (message.Text)
            {
                case "/start":

                    if(session.ProcessingMode)
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Режим обработки текста ввода уже установлен.", cancellationToken: ct);
                        break;
                    }

                    // Объект, представляющий кноки
                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    {
                        InlineKeyboardButton.WithCallbackData($" Подсчёт количества символов в тексте" , $"1"),
                        InlineKeyboardButton.WithCallbackData($" Вычисление суммы чисел" , $"2")
                    });

                    // передаем кнопки вместе с сообщением (параметр ReplyMarkup)
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, $"<b> Выберите, что	 должен делать Бот...</b> {Environment.NewLine}" +
                        $"{Environment.NewLine}Подсчёт количества символов в тексте или вычисление суммы чисел?{Environment.NewLine}" +
                            $"{Environment.NewLine}Для завершения введите /stop{Environment.NewLine}",
                            cancellationToken: ct, parseMode: ParseMode.Html, replyMarkup: new InlineKeyboardMarkup(buttons));

                    break;

                case "/stop":

                    session.ProcessingMode = false;
                    Console.WriteLine("Режим обработки текста ввода отменен.");
                    await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Режим обработки текста ввода отменен.", cancellationToken: ct);
                    break;

                default:

                    if(session.ProcessingMode)
                    {
                        string str;
                        if(session.TypeFunctions == "1")
                        {
                            str = _processInputString.Process(message.Text, 1);
                        }
                        else
                        {
                            str = _processInputString.Process(message.Text, 2);
                        }

                        Console.WriteLine($"{str}");
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, str, cancellationToken: ct);
                    }
                    else
                    {
                        await _telegramClient.SendTextMessageAsync(message.Chat.Id, "Отправте /start для режима обработки текста ввода", cancellationToken: ct);
                    }
                    break;
            }
        }
    }
}
