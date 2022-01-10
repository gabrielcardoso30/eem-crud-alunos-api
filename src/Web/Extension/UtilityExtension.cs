using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Core.Interfaces;
using Core.Interfaces.Security;
using Core.Security;
using Infra.Data;

namespace Web.Extension
{
    public static class UtilityExtension
    {
        public static IServiceCollection AddUtility(
            this IServiceCollection services)
        {
            // Configuração de Cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            // Configuração de Cors
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            
            // Middleware de autorização
            services.AddTransient<TokenManagerMiddleware>();
            services.AddTransient<ITokenManager, TokenManager>();
            
            // Recuperar usuario logado
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<AuthenticatedUser>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddHttpClient();
            //services.AddSingleton<HttpClient>();
            
            return services;
        }
    }
}