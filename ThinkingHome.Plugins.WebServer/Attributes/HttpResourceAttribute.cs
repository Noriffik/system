using System;

namespace ThinkingHome.Plugins.WebServer.Attributes;

public abstract class HttpResourceAttribute(string url) : Attribute {
    public string Url { get; } = url;
}
