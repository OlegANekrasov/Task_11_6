using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task_11_6.Services;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Task_11_6.Controllers
{
    public class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null)
                return;

            // Обновление пользовательской сессии новыми данными
            var session = _memoryStorage.GetSession(callbackQuery.From.Id);
            session.TypeFunctions = callbackQuery.Data;
            session.ProcessingMode = true;

            // Генерим информационное сообщение
            string languageText = callbackQuery.Data switch
            {
                "1" => " Подсчёт количества символов в тексте",
                "2" => " Вычисление суммы чисел",
                _ => String.Empty
            };

            Console.WriteLine($"Режим Бота - {languageText}.");

            // Отправляем в ответ уведомление о выборе
            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Режим Бота - {languageText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
