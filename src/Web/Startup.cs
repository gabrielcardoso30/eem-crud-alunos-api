using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infra.Data;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Core.Helpers;
using Core.Security;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.EntityFrameworkCore.Storage;
using Web.Extension;
using Microsoft.Extensions.Logging;
using DinkToPdf.Contracts;
using DinkToPdf;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration config, IHostEnvironment env)
        {
            Configuration = config;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                .AddEnvironmentVariables();

            Configuration.AddSerilog();
            Log.Information($"Aplicação iniciada. Ambiente: {env.EnvironmentName}.");

            Configuration = builder.Build();
        }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(assembly);

            services.AddAntiforgery(options => 
            {
                // Set Cookie properties using CookieBuilder properties†.
                options.FormFieldName = "AntiforgeryFieldname";
                options.HeaderName = "X-CSRF-TOKEN-HEADERNAME";
                options.SuppressXFrameOptionsHeader = false;
            });

            var ApplicationInsightsKey = SettingsConfigurationHelper.RetornaValor("APPLICATIONINSIGHTSKEY", Configuration);
            var connectionDbHost = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBHOST",Configuration);
            var connectionDbPort = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBPORT",Configuration);
            var connectionDbDatabase = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBDATABASE",Configuration);
            var connectionDbUserId = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBUSERID",Configuration);
            var connectionDbPassword = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBPASSWORD",Configuration);
            var connectionDbTimeout = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBTIMEOUT", Configuration);
            var connectionDbTrustServerCertificate = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBTRUSTSERVERCERTIFICATE", Configuration);
            var connectionDbEncrypt = SettingsConfigurationHelper.RetornaValor("CONNECTIONDBENCRYPT", Configuration);

            var defaultConnection = $"Data Source={connectionDbHost};" +
                                        //$"Port={connectionDbPort};" +
                                        $"Initial Catalog={connectionDbDatabase};" +
                                        $"User ID={connectionDbUserId};" +
                                        $"Password={connectionDbPassword};" +
                                        $"Connect Timeout={connectionDbTimeout};" +
                                        $"Encrypt={connectionDbEncrypt};" +
                                        $"TrustServerCertificate={connectionDbTrustServerCertificate};";
            //var defaultConnection = SettingsConfigurationHelper.RetornaValor("DEFAULTCONNECTION", Configuration);
            services.Configure<AppSettings>(Configuration.GetSection("APPSETTINGS"));
            
            services.AddEntityConfiguration(defaultConnection);
            //services.AddRedisConfiguration(Configuration);
            services.AddVariables();
            services.AddUtility();
            services.AddSecurity(Configuration, defaultConnection);
            services.AddPolicy();

            services.AddSwagger();

            services.AddCompression();
            services.AddMapping();
            services.AddAutoMapper(assembly);
            services.AddHealthChecks();

            services.AddApplicationInsightsTelemetry(ApplicationInsightsKey);

            services.AddMvc(options => { options.EnableEndpointRouting = false; })
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>())
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                });

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            return BuildDependencyInjectionProvider(services);
        }

        private static IServiceProvider BuildDependencyInjectionProvider(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            // Populate the container using the service collection
            builder.Populate(services);

            var webAssembly = Assembly.GetExecutingAssembly();
            var coreAssembly = Assembly.GetAssembly(typeof(AccessManager));
            var infraAssembly = Assembly.GetAssembly(typeof(AppDbContext));

            builder.RegisterAssemblyTypes(webAssembly, coreAssembly, infraAssembly)
                .AsImplementedInterfaces();

            var applicationContainer = builder.Build();

            return new AutofacServiceProvider(applicationContainer);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            InitializeDatabase(app);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();

                if (env.IsProduction())
                {
                    app.Use(async (context, next) =>
                    {
                        //context.Response.Headers.Add("Content-Security-Policy", "default-src https: data: 'unsafe-inline' 'unsafe-eval'; script-src 'self'; style-src 'self'; img-src 'self';");
                        
                        //context.Response.Headers.Add("X-Frame-Options", "DENY");
                        //context.Response.Headers.Add("X-Xss-Protection", "1");
                        //context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                        context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                        
                        //context.Response.Headers.Add("Feature-Policy", "camera ‘none’; microphone ‘none’; speaker ‘self’; vibrate ‘none’; geolocation ‘none’; accelerometer ‘none’; ambient-light-sensor ‘none’; gyroscope ‘none’; magnetometer ‘none’;");
                        await next();
                    });
                }
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthorization();
            app.UseAuthentication();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.

            app.UseSwagger();

            if (!env.IsProduction())
            {
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1 - " + env.EnvironmentName);
                    c.RoutePrefix = "";
                    c.DocExpansion(DocExpansion.None);
                });
            }
            
            app.UseReDoc(c =>
            {
                c.DocumentTitle = "REDOC Identity Documentation";
                c.SpecUrl = "/swagger/v1/swagger.json";
                c.RoutePrefix = "doc";
            }); 

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                Log.Error(error.Error, "Erro");
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });

            app.UseHealthChecks("/status",
                new HealthCheckOptions()
                {
                    ResponseWriter = async (context, report) =>
                    {
                        var result = JsonConvert.SerializeObject(
                            new
                            {
                                statusApplication = report.Status.ToString(),
                                healthChecks = report.Entries.Select(e => new
                                {
                                    check = e.Key,
                                    ErrorMessage = e.Value.Exception?.Message,
                                    status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                                })
                            });
                        context.Response.ContentType = MediaTypeNames.Application.Json;
                        await context.Response.WriteAsync(result);
                    }
                });
           
            // Gera o endpoint que retornará os dados utilizados no dashboard
            app.UseHealthChecks("/hc-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
            });
            
            app.UseResponseCompression();
            app.UseMiddleware<TokenManagerMiddleware>();
            app.UseMvc();
            
            loggerFactory.AddApplicationInsights(app.ApplicationServices, LogLevel.Information);
        }

        private class BrotliCompressionProvider : ICompressionProvider
        {
            public string EncodingName => "br";
            public bool SupportsFlush => true;

            public Stream CreateStream(Stream outputStream) =>
                new BrotliStream(outputStream, CompressionLevel.Optimal, true);
        }

        public class CustomNameSqlGenerationHelper : RelationalSqlGenerationHelper
        {
            private static string Customize(string input) => input.ToLower();
            public CustomNameSqlGenerationHelper(RelationalSqlGenerationHelperDependencies dependencies) : base(dependencies) { }
            public override string DelimitIdentifier(string identifier) => base.DelimitIdentifier(Customize(identifier));
            public override void DelimitIdentifier(StringBuilder builder, string identifier) => base.DelimitIdentifier(builder, Customize(identifier));

        }
        
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();
            }
        }
    }
}