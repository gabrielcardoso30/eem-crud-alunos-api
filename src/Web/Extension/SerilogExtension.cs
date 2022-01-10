using System;
using Core.Helpers;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace Web.Extension
{
    public static class SerilogExtension
    {
        private static string elasticHost = null;
        private static string elasticPort = null;
        private static string elasticUsername = null;
        private static string elasticPassword = null;
        private static string applicationInsightsKey = null;

        public static void AddSerilog(
            this IConfiguration configuration)
        {
            var urlElasticSearch = GetElasticUrl(configuration);
            if (!IsValidConfiguration(urlElasticSearch))
            {
                return;
            }

            applicationInsightsKey = SettingsConfigurationHelper.RetornaValor("ELASTICPASSWORD", configuration);
            var telemetryConfiguration = TelemetryConfiguration
            .CreateDefault();
            telemetryConfiguration.InstrumentationKey = applicationInsightsKey;

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")
                // .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(urlElasticSearch))
                // {
                //     AutoRegisterTemplate = true
                // })
                //.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day, shared: true)
                .WriteTo.ApplicationInsights(telemetryConfiguration, TelemetryConverter.Traces)
                .CreateLogger();
        }

        private static string GetElasticUrl(IConfiguration configuration)
        {
            elasticHost = SettingsConfigurationHelper.RetornaValor("ELASTICHOST", configuration);
            elasticPort = SettingsConfigurationHelper.RetornaValor("ELASTICPORT", configuration);
            elasticUsername = SettingsConfigurationHelper.RetornaValor("ELASTICUSERNAME", configuration);
            elasticPassword = SettingsConfigurationHelper.RetornaValor("ELASTICPASSWORD", configuration);

            if (!IsValidConfiguration(elasticHost) || !IsValidConfiguration(elasticPort) || !IsValidConfiguration(elasticUsername) || !IsValidConfiguration(elasticPassword))
            {
                return null;
            }

            return $"https://{elasticUsername}:{elasticPassword}@{elasticHost}:{elasticPort}/";
        }

        private static bool IsValidConfiguration(string config)
        {
            return (string.IsNullOrEmpty(config) || string.IsNullOrWhiteSpace(config)) ? false : true;
        }
    }
}