using System;

namespace ThinkingHome.Plugins.TelegramBot;

public class TelegramMessageHandlerAttribute(string command) : Attribute {
    public const string ALL_COMMANDS = "F8AA75301025444B90147CD0FAE48312";

    public TelegramMessageHandlerAttribute() : this(ALL_COMMANDS)
    {
    }

    public string Command { get; } = (command ?? string.Empty).TrimStart('/');
}
