using System;

namespace ThinkingHome.Plugins.TelegramBot.Model;

public class Chat {
    public Guid Id { get; set; }
    
    public string Login { get; set; }
    
    public long ChatId { get; set; }
}
