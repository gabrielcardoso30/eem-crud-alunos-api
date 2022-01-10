using System;
using Core.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Web.Extension
{
    public static class RedisConfigurationExtension
    {
        private static string redisServer = null;
        private static string redisPort = null;
        private static string redisSsl = null;
        private static string redisPassword = null;

        public static IServiceCollection AddRedisConfiguration(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            return GetRedisService(services, configuration);
        }

        private static IServiceCollection GetRedisService(IServiceCollection services, IConfiguration configuration)
        {
            redisServer = SettingsConfigurationHelper.RetornaValor("REDISSERVER", configuration);
            redisPort = SettingsConfigurationHelper.RetornaValor("REDISPORT", configuration);
            redisSsl = SettingsConfigurationHelper.RetornaValor("REDISSSL", configuration);
            redisPassword = SettingsConfigurationHelper.RetornaValor("REDISPASSWORD", configuration);

            if (!IsValidConfiguration(redisServer) || !IsValidConfiguration(redisPort) || !IsValidConfiguration(redisSsl) || !IsValidConfiguration(redisPassword))
            {
                return null;
            }

            services.AddStackExchangeRedisCache(options =>
            {
                options.ConfigurationOptions = new ConfigurationOptions()
                {
                    EndPoints = { redisServer + ":" + redisPort },
                    AllowAdmin = true,
                    AbortOnConnectFail = false,
                    DefaultDatabase = 0,
                    ConnectTimeout = 500,
                    ConnectRetry = 3,
                    Ssl = Convert.ToBoolean(redisSsl),
                    Password = redisPassword
                };
            });

            return services;
        }

        private static bool IsValidConfiguration(string config)
        {
            return (string.IsNullOrEmpty(config) || string.IsNullOrWhiteSpace(config)) ? false : true;
        }
    }
}