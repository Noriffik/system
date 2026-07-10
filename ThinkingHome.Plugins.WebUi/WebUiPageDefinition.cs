using ThinkingHome.Core.Plugins.Utils;

namespace ThinkingHome.Plugins.WebUi;

public class WebUiPageDefinition(Type source, string url, string jsResourcePath, string langId) {
    public readonly string PathDocument = url;
    public readonly string PathJavaScript = $"/static/webui/js/{url.GetHashString()}.js";

    public readonly Type Source = source;
    public readonly string JsResourcePath = jsResourcePath;
    public readonly string LangId = langId;
}
