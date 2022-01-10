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
    public class PermissaoGrupoController : BaseApiController
    {
        private readonly IMediator _mediator;

        public PermissaoGrupoController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [HttpResultActionFilter]
        [Authorize(Policy ="ConsultarPermissoesGrupo")]
        public async Task<IActionResult> Get([FromQuery] GetPermissoesGrupoQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy ="DeletarPermissaoGrupo")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePermissaoGrupoCommand(id);
            var response = await _mediator.Send(command);

            return Ok(response);
        }
        
        
        [HttpPost]
        [HttpResultActionFilter]
        [Authorize(Policy ="CadastrarPermissaoGrupo")]
        public async Task<IActionResult> Create([FromBody] CreatePermissaoGrupoCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut]
        [HttpResultActionFilter]
        [Authorize(Policy = "CadastrarPermissaoGrupo")]
        public async Task<IActionResult> Update([FromBody] UpdatePermissaoGrupoCommand command)
        {

            var response = await _mediator.Send(command);
            return Ok(response);

        }

    }
}