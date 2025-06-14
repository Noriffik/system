using Microsoft.EntityFrameworkCore;
using Telegram.Bot.Types;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Database;
using ThinkingHome.Plugins.TelegramBot;

using Chat = ThinkingHome.Plugins.TelegramChatList.Model.Chat;

namespace ThinkingHome.Plugins.TelegramChatList;

public class TelegramChatListPlugin(TelegramBotPlugin telegramBot, DatabasePlugin database) : PluginBase {
    
    [DbModelBuilder]
    public void InitModel(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(cfg => cfg.ToTable("TelegramChatList_Chat"));
    }
    public override void InitPlugin()
    {
        telegramBot.OnMessageReceived += TelegramBotOnOnMessageReceived;
    }

    private void TelegramBotOnOnMessageReceived(Message msg)
    {
        using var db = database.OpenSession();

        var savedChat = db.Set<Chat>().SingleOrDefault(c => c.ChatId == msg.Chat.Id);
        
        if (savedChat == null) {
            savedChat = new Chat 
                { Id = Guid.NewGuid(), ChatId = msg.Chat.Id, Login = msg.Chat.Username };
            
            db.Set<Chat>().Add(savedChat);
        }
        else {
            savedChat.Login = msg.Chat.Username;
        }

        db.SaveChanges();
    }

    public override void StartPlugin()
    {
        telegramBot.SendMessage(353206782, "чатлисту весело он работает !");
    }
}
