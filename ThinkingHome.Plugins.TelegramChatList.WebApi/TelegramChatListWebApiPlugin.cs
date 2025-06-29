using Microsoft.Extensions.Logging;
using ThinkingHome.Core.Plugins;

namespace ThinkingHome.Plugins.TelegramChatList.WebApi;

public class TelegramChatListWebApiPlugin() : PluginBase {
    public override void InitPlugin()
    {
    }

    public override void StartPlugin()
    {
        Logger.LogInformation("юхуу плагин апишки чат листа работаееееет ееееееее");
    }

    public override void StopPlugin()
    {
    }
}
