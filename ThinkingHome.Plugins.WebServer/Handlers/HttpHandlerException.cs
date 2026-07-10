using System;

namespace ThinkingHome.Plugins.WebServer.Handlers;

public enum StatusCode {
    BadRequest = 400,
    Internal = 500,
}

public class HttpHandlerException(StatusCode statusCode, string message = null) : Exception(message) {
    public StatusCode StatusCode { get; } = statusCode;
}
