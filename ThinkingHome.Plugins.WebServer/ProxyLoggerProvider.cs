using Microsoft.Extensions.Logging;

namespace ThinkingHome.Plugins.WebServer;

public class ProxyLoggerProvider(ILogger logger) : ILoggerProvider {
    public ILogger CreateLogger(string categoryName) => logger;

    public void Dispose()
    {
    }
}

public static class ProxyLoggerProviderExtensions {
        public static ILoggingBuilder AddProxy(this ILoggingBuilder builder, ILogger logger)
        {
            builder.AddProvider(new ProxyLoggerProvider(logger));
            return builder;
        }
    }
