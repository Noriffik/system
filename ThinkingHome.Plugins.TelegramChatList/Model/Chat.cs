namespace ThinkingHome.Plugins.TelegramChatList.Model;

public class Chat {
    public Guid Id { get; init; }
    
    public string? Login { get; set; }
    
    public long ChatId { get; init; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public DateTime Date { get; set; }
}
