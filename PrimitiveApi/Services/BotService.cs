using Microsoft.Extensions.Options;
using PrimitiveApi.Dto;
using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace PrimitiveApi.Services;

public interface IBotService
{
    TelegramBotClient Bot { get; }
    Task SendTextMessage(string text, long chatId);
    void Subscribe(long chatId);
    Task SendAll(string text);
}

public class BotService(IOptions<TelegramBotConfig> options) : IBotService
{
    public TelegramBotClient Bot { get; } = new (options.Value.BotToken);
    
    private readonly HashSet<long> _subscribedUsers = new();

    public async Task SendTextMessage(string text, long chatId) => await Bot.SendMessageAsync(chatId, text);

    public async Task SendAll(string text)
    {
        var tasks = _subscribedUsers.Select(x=>SendTextMessage(text, x));
        await Task.WhenAll(tasks);
    }

    public void Subscribe(long chatId) => _subscribedUsers.Add(chatId);
    
}