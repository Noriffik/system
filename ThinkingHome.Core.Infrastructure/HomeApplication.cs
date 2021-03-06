﻿using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ThinkingHome.Core.Plugins;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace ThinkingHome.Core.Infrastructure
{
    public class HomeApplication
    {
        private IServiceProvider services;

        private ILogger logger;

        private IServiceContext context;

        public void StartServices(HomeConfiguration config)
        {
            services = ConfigureServices(config);

            var loggerFactory = services
                .GetRequiredService<ILoggerFactory>()
                .AddSerilog(config.LoggerConfiguration.CreateLogger(), true);

            logger = loggerFactory.CreateLogger<HomeApplication>();
            context = services.GetRequiredService<IServiceContext>();

            try
            {
                // init plugins
                foreach (var plugin in context.GetAllPlugins())
                {
                    logger.LogInformation($"init plugin: {plugin.GetType().FullName}");
                    plugin.InitPlugin();
                }

                // start plugins
                foreach (var plugin in context.GetAllPlugins())
                {
                    logger.LogInformation($"start plugin {plugin.GetType().FullName}");
                    plugin.StartPlugin();
                }

                logger.LogInformation("all plugins are started");
            }
            catch (ReflectionTypeLoadException ex)
            {
                logger.LogError(0, ex, "error on plugins initialization");
                foreach (var loaderException in ex.LoaderExceptions)
                {
                    logger.LogError(0, loaderException, loaderException.Message);
                }
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(0, ex, "error on start plugins");
                throw;
            }
        }

        public void StopServices()
        {
            foreach (var plugin in context.GetAllPlugins())
            {
                try
                {
                    logger.LogInformation($"stop plugin {plugin.GetType().FullName}");
                    plugin.StopPlugin();
                }
                catch (Exception ex)
                {
                    logger.LogError(0, ex, "error on stop plugins");
                }
            }

            logger.LogInformation("all plugins are stopped");
        }

        #region private

        private IServiceProvider ConfigureServices(HomeConfiguration config)
        {
            var asms = config.GetDependencies().ToArray();

            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton<ILoggerFactory, LoggerFactory>();
            serviceCollection.AddSingleton<IServiceContext, ServiceContext>();
            serviceCollection.AddSingleton<IConfigurationSection>(config.Configuration.GetSection("plugins"));

            foreach (var asm in asms)
            {
                AddAssemblyPlugins(serviceCollection, asm);
            }

            return serviceCollection.BuildServiceProvider();
        }

        private void AddAssemblyPlugins(ServiceCollection services, Assembly asm)
        {
            var baseType = typeof(PluginBase);

            foreach (var pluginType in asm.GetExportedTypes().Where(type => baseType.GetTypeInfo().IsAssignableFrom(type)))
            {
                services.AddSingleton(baseType, pluginType);
            }
        }

        #endregion
    }
}