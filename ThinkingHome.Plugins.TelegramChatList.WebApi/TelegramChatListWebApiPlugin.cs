using Microsoft.Extensions.Logging;
using ThinkingHome.Core.Plugins;
using ThinkingHome.Plugins.WebServer;
using ThinkingHome.Plugins.WebServer.Attributes;
using ThinkingHome.Plugins.WebServer.Handlers;

namespace ThinkingHome.Plugins.TelegramChatList.WebApi;

public class TelegramChatListWebApiPlugin() : PluginBase {
    [ConfigureWebServer]
    public void RegisterHttpHandlers(WebServerConfigurationBuilder config)
    {
        config.RegisterDynamicResource("/api/telegram-chat-list/web-api/list", GetChatList);
    }
    
    private HttpHandlerResult GetChatList(HttpRequestParams request)
    {
        var response = "тттттт муму";
        return HttpHandlerResult.Json(response);
    }
}
