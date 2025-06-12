using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.TelegramBot;

namespace ThinkingHome.Plugins.TelegramChatList;

public class TelegramChatListPlugin(TelegramBotPlugin telegramBot) : PluginBase {
    public override void InitPlugin()
    {
        
    }

    public override void StartPlugin()
    {
        telegramBot.SendMessage(353206782, "чатлисту весело он работает !");
    }
}
