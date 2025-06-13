using Telegram.Bot.Types;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.TelegramBot;

namespace ThinkingHome.Plugins.TelegramChatList;

public class TelegramChatListPlugin(TelegramBotPlugin telegramBot) : PluginBase {
    public override void InitPlugin()
    {
        telegramBot.OnMessageReceived += TelegramBotOnOnMessageReceived;
    }

    private void TelegramBotOnOnMessageReceived(Message message)
    {
        telegramBot.SendMessage(353206782, $"пришло новое сообщение от @{message.Chat.Username}");
    }

    public override void StartPlugin()
    {
        telegramBot.SendMessage(353206782, "чатлисту весело он работает !");
    }
}
