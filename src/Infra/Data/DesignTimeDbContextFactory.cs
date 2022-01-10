using System;
using System.IO;
using System.Reflection;
using System.Text;
using Core.Helpers;
using Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace Infra.Data
{
    public class AppDbContextDesignFactory : DesignTimeDbContextFactoryBase<AppDbContext>
    {
        public AppDbContextDesignFactory() : base()
        {
        }

        protected override AppDbContext CreateNewInstance(
            DbContextOptions<AppDbContext> options)
        {
            return new AppDbContext(options);
        }
    }
    
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
        IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public DesignTimeDbContextFactoryBase()
        {
        }

        public TContext CreateDbContext(string[] args)
        {
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Web");
            return Create(
                basePath,
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        protected abstract TContext CreateNewInstance(
            DbContextOptions<TContext> options);

        public TContext CreateWithConnectionStringName(string connectionStringName)
        {
            var environmentName =
                Environment.GetEnvironmentVariable(
                    "ASPNETCORE_ENVIRONMENT");

            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Web");

            return Create(basePath, environmentName);
        }

        private TContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            var connectionDbHost = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBHOST", config);
            var connectionDbPort = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBPORT", config);
            var connectionDbDatabase = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBDATABASE", config);
            var connectionDbUserId = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBUSERID", config);
            var connectionDbPassword = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBPASSWORD", config);
            var connectionDbTimeout = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBTIMEOUT", config);
            var connectionDbTrustServerCertificate = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBTRUSTSERVERCERTIFICATE", config);
            var connectionDbEncrypt = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBENCRYPT", config);

            //var connectionString = SettingsConfigurationHelper.RetornaValor("DEFAULTCONNECTION", config);
            //return CreateWithConnectionString(connectionString);
            var connstr = $"Data Source={connectionDbHost};" +
                                        //$"Port={connectionDbPort};" +
                                        $"Initial Catalog={connectionDbDatabase};" +
                                        $"User ID={connectionDbUserId};" +
                                        $"Password={connectionDbPassword};" +
                                        $"Connect Timeout={connectionDbTimeout};" +
                                        $"Encrypt={connectionDbEncrypt};" +
                                        $"TrustServerCertificate={connectionDbTrustServerCertificate};";


            Console.WriteLine($"Environment: {environmentName ?? "PRODUCTION"}");

            if (string.IsNullOrWhiteSpace(connstr))
            {
                throw new InvalidOperationException(
                    $"Não foi possível encontrar a configuração da ConnectionString.");
            }
            else
            {
                return CreateWithConnectionString(connstr);
            }
        }

        private TContext CreateWithConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
                    $"A ConnectionString está vazia ou nula ({nameof(connectionString)}) .",
                    nameof(connectionString));

            var migrationsAssembly = typeof(DataHelpers).GetTypeInfo().Assembly.GetName().Name;
            
            var optionsBuilder =
                new DbContextOptionsBuilder<TContext>();

            optionsBuilder.EnableSensitiveDataLogging();
            
            Console.WriteLine("DesignTimeDbContextFactory.Create(string): ConnectionString");

            optionsBuilder.UseSqlServer(connectionString,
                b =>
                {
                    b.MigrationsAssembly(migrationsAssembly);
                    b.MigrationsHistoryTable("__efmigrationshistory");
                    b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });

            DbContextOptions<TContext> options = optionsBuilder.Options;

            return CreateNewInstance(options);
        }
        
        
        public class CustomNameSqlGenerationHelper : RelationalSqlGenerationHelper
        {
            private static string Customize(string input) => input.ToLower();
            public CustomNameSqlGenerationHelper(RelationalSqlGenerationHelperDependencies dependencies) : base(dependencies) { }
            public override string DelimitIdentifier(string identifier) => base.DelimitIdentifier(Customize(identifier));
            public override void DelimitIdentifier(StringBuilder builder, string identifier) => base.DelimitIdentifier(builder, Customize(identifier));
        }
        
    }
}