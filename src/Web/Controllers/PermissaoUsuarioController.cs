using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Commands.Security;
using Core.Queries.Security;
using Web.Api;
using Web.Filters;

namespace Web.Controllers
{
    [Authorize]
    public class PermissaoUsuarioController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PermissaoUsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [HttpResultActionFilter]
        [Authorize(Policy ="ConsultarPermissoesUsuario")]
        public async Task<IActionResult> Get([FromQuery(Name = "usuarioId")] Guid usuarioId)
        {

            GetPermissoesUsuarioQuery userPermissions = new GetPermissoesUsuarioQuery(usuarioId);
            var response = await _mediator.Send(userPermissions);
            return Ok(response);

        }

        [HttpDelete("id")]
        [HttpResultActionFilter]
        [Authorize(Policy ="DeletarPermissaoUsuario")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePermissaoUsuarioCommand(id);
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        
        
        [HttpPost]
        [HttpResultActionFilter]
        [Authorize(Policy ="CadastrarPermissaoUsuario")]
        public async Task<IActionResult> Create([FromBody] CreatePermissaoUsuarioCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut]
        [HttpResultActionFilter]
        [Authorize(Policy ="CadastrarPermissaoUsuario")]
        public async Task<IActionResult> Update([FromBody] UpdatePermissaoUsuarioCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}