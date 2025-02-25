using PrimitiveApi.Services;
using Telegram.BotAPI;
using Telegram.BotAPI.GettingUpdates;

namespace PrimitiveApi;

public class TelegramBotWorker(IBotService telegramBotService) : BackgroundService
{
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var updates = (await telegramBotService.Bot
            .GetUpdatesAsync(cancellationToken: stoppingToken)).ToArray();
        while (!stoppingToken.IsCancellationRequested)
        {
            if (updates is null) continue;
            if (updates.Any())
            {
                foreach (var update in updates)
                {
                    if (update.Message is not null)
                    {
                        await telegramBotService.SendTextMessage(
                            "Спасибо за использование нашего бота! Ожидайте уведомлений!",
                            update.Message.Chat.Id);
                        telegramBotService.Subscribe(update.Message.Chat.Id);
                    }
                    
                }
                var offset = updates.Last().UpdateId + 1;
                updates = (await telegramBotService.Bot
                    .GetUpdatesAsync(offset, cancellationToken: stoppingToken)).ToArray();
            }
            else
            {
                updates = (await telegramBotService.Bot
                    .GetUpdatesAsync(cancellationToken: stoppingToken)).ToArray();
            }
        }
    }
}