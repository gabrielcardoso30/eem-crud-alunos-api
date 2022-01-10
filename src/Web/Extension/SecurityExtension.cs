using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Entities.Security;
using Core.Security;
using Infra.Data;
using Core.Helpers;

namespace Web.Extension
{
    public static class SecurityExtension
    {
        public static IServiceCollection AddSecurity(
            this IServiceCollection services,
            IConfiguration configuration,
            string connectionString)
        {
            var tamanhoMinimoSenha = SettingsConfigurationHelper.RetornaValor("TAMANHOMINIMOSENHA", configuration);
            var tokenAudience = SettingsConfigurationHelper.RetornaValor("TOKENAUDIENCE", configuration);
            var tokenIssuer = SettingsConfigurationHelper.RetornaValor("TOKENISSUER", configuration);

            
            // Ativando a utilização do ASP.NET Identity, a fim de
            // permitir a recuperação de seus objetos via injeção de
            // dependências
            services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
                {
                    // Default Password settings.
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = Convert.ToInt32(tamanhoMinimoSenha);
                    options.Password.RequiredUniqueChars = 1;
                })
                .AddErrorDescriber<LocalizedIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Configurando a dependência para a classe de validação
            // de credenciais e geração de tokens
            services.AddScoped<AccessManager>();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);
            
            // Aciona a extensão que irá configurar o uso de autenticação e autorização via tokens
            services.AddJwtSecurity(signingConfigurations, tokenAudience, tokenIssuer);
            
            return services;
        }
    }
}