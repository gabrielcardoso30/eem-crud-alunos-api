using System;
using Microsoft.Extensions.DependencyInjection;
using Core.Helpers;

namespace Web.Extension
{
    public static class VariablesExtension
    {
        public static IServiceCollection AddVariables(
            this IServiceCollection services)
        {
            
            var kafkaServer = Environment.GetEnvironmentVariable("KAFKASERVER");
            var kafkaPort = Environment.GetEnvironmentVariable("KAFKAPORT");
            var kafkaMailTopic = Environment.GetEnvironmentVariable("KAFKAMAILTOPIC");
            var quantidadeLogin = Environment.GetEnvironmentVariable("QUANTIDADELOGIN");
            var tokenAudience = Environment.GetEnvironmentVariable("TOKENAUDIENCE");
            var tokenIssuer = Environment.GetEnvironmentVariable("TOKENISSUER");
            var tokenSeconds = Environment.GetEnvironmentVariable("TOKENSECONDS");
            var mailAccount = Environment.GetEnvironmentVariable("SMTPMAILACCOUNT");
            var mailPassword = Environment.GetEnvironmentVariable("SMTPMAILPASSWORD");
            var mailDisplayName = Environment.GetEnvironmentVariable("SMTPMAILDISPLAYNAME");
            var mailHost = Environment.GetEnvironmentVariable("SMTPMAILHOST");
            var mailPort = Environment.GetEnvironmentVariable("SMTPMAILPORT");
            var storageAccountKey = Environment.GetEnvironmentVariable("STORAGEACCOUNTNAME");
            var storageAccountName = Environment.GetEnvironmentVariable("STORAGEACCOUNTKEY");
            var storageUrlBlobFiles = Environment.GetEnvironmentVariable("STORAGEURLBLOBFILES");
            var storageConnectionString = Environment.GetEnvironmentVariable("STORAGECONNECTIONSTRING");
            var environmentVariables = new EnvironmentVariables(
                kafkaServer, 
                kafkaPort, 
                kafkaMailTopic,
                quantidadeLogin, 
                tokenAudience, 
                tokenIssuer, 
                tokenSeconds,
                mailAccount,
                mailPassword,
                mailDisplayName,
                mailHost,
                mailPort,
                storageAccountKey,
                storageAccountName,
                storageUrlBlobFiles,
                storageConnectionString
                );
            services.AddSingleton(environmentVariables);
            
            return services;
        }
    }
}