using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Core.Security
{
    public class TokenManager : ITokenManager
    {
        private readonly IDistributedCache _cache;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<ApplicationUser> _userManager;

        public TokenManager(IDistributedCache cache,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<bool> IsCurrentActiveToken()
        {
            var current = GetIdCurrentAsync();
            var tokenType = GetTokenTypeCurrentAsync();

            if (current == null) return true;

            //QUANDO O REDIS ESTIVER ATIVO
            //var token = await GetActiveAsync(current, tokenType);
            //var accessToken = JsonConvert.DeserializeObject<Token>(token);            
            //return accessToken.AccessToken == GetCurrentAsync();

            //QUANDO O REDIS ESTIVER INATIVO
            var token = await GetActiveAsync(current, tokenType);
            return token == GetCurrentAsync();

        }

        public async Task ActivateAsync(string id, string token)
        {
            await _cache.SetStringAsync(id, token);
        }

        public async Task<string> GetActiveAsync(string id, string tokenType)
        {

            //QUANDO O REDIS ESTIVER ATIVO
            //return await _cache.GetStringAsync(id); 

            //QUANDO O REDIS ESTIVER INATIVO
            var user = await _userManager.FindByIdAsync(id);
            var token = await _userManager.GetAuthenticationTokenAsync(user,
                "Token",
                tokenType);
            return token;

        }

        private string GetCurrentAsync()
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["authorization"];

                return authorizationHeader == StringValues.Empty
                    ? string.Empty
                    : authorizationHeader.Single().Split(' ').Last();
            }

            return string.Empty;
        }

        private string GetIdCurrentAsync()
        {
            return _httpContextAccessor.HttpContext != null 
                ? _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "id")?.Value
                : string.Empty;
        }
        
        string GetTokenTypeCurrentAsync()
        {
            return _httpContextAccessor.HttpContext != null 
                ? _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(a => a.Type == "type")?.Value
                : string.Empty;
        }
    }
}