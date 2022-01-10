using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Interfaces.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Core.Security
{

    public class TokenManagerMiddleware : IMiddleware
    {
        private readonly ITokenManager _tokenManager;
        private readonly ILogger<TokenManagerMiddleware> _logger;

        public TokenManagerMiddleware(ITokenManager tokenManager, ILogger<TokenManagerMiddleware> logger)
        {
            _tokenManager = tokenManager;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {            
            try
            {
                if (await _tokenManager.IsCurrentActiveToken() || context.Request.Path == "/api/login" )
                {
                    await next(context);

                    return;
                }
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            }
            catch (Exception ex)
            {
                string erro = ex.InnerException?.Message ?? ex.Message;
                _logger.LogError(ex, erro);
                //Log.Error(ex, erro);
                await HandlerExceptionAsync(context, ex);
            }      
        }
        private static Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            
            var code = HttpStatusCode.InternalServerError;
            //var msgErro = "Ocorreu um erro no sistema. Contate o administrador do sistema.";
            var msgErro = exception.InnerException?.Message ?? exception.Message;
            var result = JsonSerializer.Serialize(msgErro);
            // var options = new JsonSerializerOptions { WriteIndented = true };
            // var result = JsonSerializer.Serialize(exception, options);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            return context.Response.WriteAsync(result);
        }
    }
}
