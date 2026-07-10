using ThinkingHome.Core.Plugins.Utils;

namespace ThinkingHome.Plugins.WebUi;

public class WebUiConfigurationBuilder(Type source, string langId, ObjectRegistry<WebUiPageDefinition> pages)
    : BaseConfigurationBuilder<WebUiPageDefinition>(source, pages) {
    public bool HasPages { get; private set; }

    public WebUiConfigurationBuilder RegisterPage(string url, string jsEmbeddedResourcePath)
    {
        RegisterItem(url, new WebUiPageDefinition(Source, url, jsEmbeddedResourcePath, langId));

        HasPages = true;

        return this;
    }
}
