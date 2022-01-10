using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Core.Security
{
    public class AccessManager
    {
        private SignInManager<ApplicationUser> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private EnvironmentVariables _environmentVariables;
        private AppSettings _appSettings;

        public AccessManager(
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            EnvironmentVariables environmentVariables,
            IOptions<AppSettings> appSettings)
        {
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _environmentVariables = environmentVariables;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> ValidateCredentials(ApplicationUser userIdentity, string password)
        {
            // Efetua o login com base no Id do usuário e sua senha
            var resultadoLogin =
                await _signInManager.CheckPasswordSignInAsync(userIdentity, password, false);
            
            return resultadoLogin.Succeeded;
        }

        public Token GenerateToken(ApplicationUser user, IEnumerable<PermissaoUsuario> listaAcessoUsuario,
            IEnumerable<PermissaoGrupo> listaAcessoGrupo, bool tokenTimeApp)
        {
            var listaPermissoes = listaAcessoUsuario.Select(s => s.Permissao.Nome).ToList();
            listaPermissoes.AddRange(listaAcessoGrupo.Select(s => s.Permissao.Nome));
            var permission = string.Join(";", listaPermissoes.Distinct().Select(s => s));
            
            var listClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim("id" , user.Id.ToString()),
                new Claim("permission", permission),
                new Claim("type", tokenTimeApp ? "App" : "Backoffice")
            };
            
            var identity = new ClaimsIdentity(new GenericIdentity(user.UserName, "Login"),listClaims);

            
            var tempoTokenExpired = Convert.ToInt32(_environmentVariables.TokenSeconds ?? _appSettings.TokenSeconds);
            
            var dataCriacao = DateTime.Now;
            var dataExpiracao = dataCriacao +
                                TimeSpan.FromSeconds(tempoTokenExpired);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _environmentVariables.TokenIssuer ?? _appSettings.TokenIssuer,
                Audience = _environmentVariables.TokenAudience ?? _appSettings.TokenAudience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);
            var refreshToken = GenerateRefreshToken();
            
            return new Token
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                RefreshToken = refreshToken,
                Message = "OK"
            };
        }
        
        public static string GeneratePassword(int qtdCaracteresMin, int qtdCaracteresMax)
        {
            const string especiais = @"!@#$%&*";
            const string caixaBaixa = "abcdefghijklmnopqrstuvwxyz";
            const string caixaAlta = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "0123456789";

            var guid = Guid.NewGuid().ToString().Replace("-", "");

            var clsRan = new Random();
            var tamanhoSenha = clsRan.Next(qtdCaracteresMin, qtdCaracteresMax);

            var senha = "";
            senha += caixaAlta.Substring(clsRan.Next(1, caixaAlta.Length), 1);
            senha += caixaBaixa.Substring(clsRan.Next(1, caixaBaixa.Length), 1);
            senha += especiais.Substring(clsRan.Next(1, especiais.Length), 1);
            senha += numeros.Substring(clsRan.Next(1, numeros.Length), 1);
            for (var i = 3; i < tamanhoSenha; i++)
            {
                senha += guid.Substring(clsRan.Next(1, guid.Length), 1);
            }

            return senha;
        }
        
        public static string GenerateNumericPassword(int tamanhoSenha)
        {
            var clsRan = new Random();
            var senha = "";
            for (var i = 0; i < tamanhoSenha; i++)
            {
                senha += clsRan.Next(0, 9).ToString();
            }
            return senha.ToString();
        }
        
        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create()){
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}