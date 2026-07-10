using System;
using Microsoft.AspNetCore.Http;
using ThinkingHome.Core.Plugins.Utils;

namespace ThinkingHome.Plugins.WebServer.Handlers;

public class HttpRequestParams {
    public readonly PathString Path;
    private readonly IQueryCollection urlData;
    private readonly IFormCollection formData;

    public HttpRequestParams(HttpRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        Path = request.Path;
        urlData = request.Query;
        formData = request.HasFormContentType ? request.Form : null;
    }

    public HttpRequestParams(PathString path, IQueryCollection urlData, IFormCollection formData)
    {
        Path = path;
        this.urlData = urlData;
        this.formData = formData;
    }

    #region params

    public string GetString(string name)
    {
        // важно неявное приведение к string, т.к. ToString
        // дополнительно заменяет null на пустую строку
        string urlValue = urlData?[name];
        string formValue = formData?[name];

        if (string.IsNullOrWhiteSpace(urlValue)) {
            return formValue;
        }

        return string.IsNullOrWhiteSpace(formValue) ? urlValue : $"{urlValue},{formValue}";
    }

    public int? GetInt32(string name)
    {
        return GetString(name).ParseInt();
    }

    public Guid? GetGuid(string name)
    {
        return GetString(name).ParseGuid();
    }

    private bool? GetBool(string name)
    {
        return GetString(name).ParseBool();
    }

    #endregion

    #region required params

    public string GetRequiredString(string name)
    {
        var value = GetString(name);

        return string.IsNullOrEmpty(value) ? throw new HttpHandlerException(StatusCode.BadRequest, $"parameter {name} is required") : value;
    }

    public int GetRequiredInt32(string name)
    {
        var value = GetInt32(name);

        return value ?? throw new HttpHandlerException(StatusCode.BadRequest, $"parameter {name} is required");
    }

    public Guid GetRequiredGuid(string name)
    {
        var value = GetGuid(name);

        return value ?? throw new HttpHandlerException(StatusCode.BadRequest, $"parameter {name} is required");
    }

    public bool GetRequiredBool(string name)
    {
        var value = GetBool(name);

        return value ?? throw new HttpHandlerException(StatusCode.BadRequest, $"parameter {name} is required");
    }

    #endregion
}
