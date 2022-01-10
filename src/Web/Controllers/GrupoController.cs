using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Commands.Security;
using Core.Queries.Security;
using Web.Api;
using Web.Filters;
using Core.Models.Filters;

namespace Web.Controllers
{
    [Authorize]
    public class GrupoController : BaseApiController
    {
        private readonly IMediator _mediator;

        public GrupoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Modulos")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarGrupo")]
        public async Task<IActionResult> GetModules()
        {

            var command = new GetModulosQuery();
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("ModulosByUnidadeAcessoId")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarGrupo")]
        public async Task<IActionResult> GetModulesByGroupId(Guid id)
        {

            var command = new GetModulosByUnidadeAcessoIdQuery(id);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet]
        [Authorize(Policy ="ConsultarGrupo")]
        [HttpResultActionFilter]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetGruposQuery());

            return Ok(response);
        }

        [HttpGet("Search")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarGrupo")]
        public async Task<IActionResult> Search([FromQuery] BaseRequestFilter filter)
        {

            var command = new GetGrupoByFilterQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy ="ConsultarGrupo")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetGrupoByIdQuery(id);
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpPut]
        [HttpResultActionFilter]
        [Authorize(Policy = "AtualizarGrupo")]
        public async Task<IActionResult> Update(
            //Guid id,
            [FromBody] UpdateGrupoCommand group
        )
        {
            //group.Id = id;
            var response = await _mediator.Send(group);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy ="DeletarGrupo")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteGrupoCommand(id, null);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("DeleteItems")]
        [HttpResultActionFilter]
        [Authorize(Policy = "DeletarGrupo")]
        public async Task<IActionResult> DeleteItems(
            [FromBody] DeleteGrupoCommand command
        )
        {

            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost]
        [HttpResultActionFilter]
        [Authorize(Policy ="CadastrarGrupo")]
        public async Task<IActionResult> Create([FromBody] CreateGrupoCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}