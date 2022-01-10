using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Commands.Security;
using Core.Queries.Security;
using Swashbuckle.AspNetCore.Annotations;
using Web.Api;
using Web.Filters;

namespace Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LoginController : BaseApiController
    {
        private readonly IMediator _mediator;

        public LoginController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [SwaggerOperation(
            Summary = "Login do sistema",
            Description = "Método responsável por efetuar o login no sistema")]
        [AllowAnonymous]
        [HttpPost]
        [HttpResultActionFilter]
        public async Task<IActionResult> Login([FromBody] CreateLoginCommand command)
        {

            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [SwaggerOperation(
            Summary = "Reset de senha",
            Description = "Método responsável por efetuar resetar a senha do usuário")]
        [AllowAnonymous]
        [HttpPost("senha/resetar")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ResetarSenhaUsuario")]
        public async Task<IActionResult> ResetarSenha([FromBody] CreateResetarSenhaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [SwaggerOperation(
            Summary = "Alterar senha",
            Description = "Método responsável por alterar a senha do usuário")]
        [AllowAnonymous]
        [HttpPost("senha/alterar")]
        [HttpResultActionFilter]
        public async Task<IActionResult> AlterarSenha([FromBody] CreateAlterarSenhaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [SwaggerOperation(
            Summary = "Logout do sistema",
            Description = "Método responsável por efetuar o logout no sistema")]
        [HttpPost("Logout")]
        [HttpResultActionFilter]
        public async Task<IActionResult> Logoff()
        {
            var command = new CreateLogoutCommand();
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [SwaggerOperation(
            Summary = "Permissões disponíveis",
            Description = "Método responsável por listar todas as permissões disponível no sistema")]
        [HttpGet("permissoes")]
        [HttpResultActionFilter]
        public async Task<IActionResult> GetPermissoes()
        {
            var command = new GetPermissoesQuery();
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}