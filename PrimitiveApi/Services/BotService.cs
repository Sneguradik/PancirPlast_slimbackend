using Telegram.BotAPI;
using Telegram.BotAPI.AvailableMethods;

namespace PrimitiveApi.Services;

public interface IBotService
{
    TelegramBotClient Bot { get; }
    Task SendTextMessage(string text, long chatId);
    void Subscribe(long chatId);
}

public class BotService(string token) : IBotService
{
    public TelegramBotClient Bot { get; } = new (token);
    
    private List<long> _subscribedUsers = new();

    public async Task SendTextMessage(string text, long chatId) => await Bot.SendMessageAsync(chatId, text);

    public async Task SendAll(string text)
    {
        var tasks = _subscribedUsers.Select(x=>SendTextMessage(text, x));
    }

    public void Subscribe(long chatId) => _subscribedUsers.Add(chatId);
    
}