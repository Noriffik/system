using System.Collections.Generic;

namespace ThinkingHome.Core.Plugins;

public interface IServiceContext {
    IReadOnlyCollection<PluginBase> GetAllPlugins(PluginsOrder order = PluginsOrder.Direct);
}
