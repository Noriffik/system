using Microsoft.Extensions.Logging;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.Database;
using ThinkingHome.Plugins.TelegramChatList.Model;
using ThinkingHome.Plugins.WebServer;
using ThinkingHome.Plugins.WebServer.Attributes;
using ThinkingHome.Plugins.WebServer.Handlers;

namespace ThinkingHome.Plugins.TelegramChatList.WebApi;

public class TelegramChatListWebApiPlugin(DatabasePlugin database) : PluginBase {
    [ConfigureWebServer]
    public void RegisterHttpHandlers(WebServerConfigurationBuilder config)
    {
        config.RegisterDynamicResource("/api/telegram-chat-list/web-api/list", GetChatList);
    }
    
    private HttpHandlerResult GetChatList(HttpRequestParams request)
    {
        using var db = database.OpenSession();
        var list = db.Set<Chat>().ToArray();
        return HttpHandlerResult.Json(list);
    }
}
