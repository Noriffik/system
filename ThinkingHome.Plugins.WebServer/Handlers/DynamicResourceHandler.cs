using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ThinkingHome.Plugins.WebServer.Handlers;

public class DynamicResourceHandler(Type source, HttpHandlerDelegate method, bool isCached) : BaseHandler(source, isCached) {
    private readonly HttpHandlerDelegate method = method ?? throw new ArgumentNullException(nameof(method));

    public override async Task<HttpHandlerResult> GetContent(HttpContext context)
    {
        var parameters = new HttpRequestParams(context.Request);

        return await Task.Run(() => method(parameters));
    }
}
