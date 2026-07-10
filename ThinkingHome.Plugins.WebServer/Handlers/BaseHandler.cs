using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ThinkingHome.Plugins.WebServer.Handlers;

public abstract class BaseHandler(Type source, bool isCached) {
    public bool IsCached { get; } = isCached;
    public Type Source { get; } = source;

    public abstract Task<HttpHandlerResult> GetContent(HttpContext context);
}
