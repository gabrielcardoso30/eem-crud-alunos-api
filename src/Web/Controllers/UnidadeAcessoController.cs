using Core.Commands.Security;
using Core.Models.Filters;
using Core.Models.Responses.Security;
using Core.Models.Request.Security;
using Core.Queries.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Api;
using Web.Filters;

namespace Web.Controllers
{

    [Authorize]
    public class UnidadeAcessoController : BaseApiController
    {

        private readonly IMediator _mediator;

        public UnidadeAcessoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Modulos")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUnidadeAcesso")]
        public async Task<IActionResult> GetModules()
        {

            var command = new GetModulosQuery();
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("ModulosByUnidadeAcessoId")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUnidadeAcesso")]
        public async Task<IActionResult> GetModulesByAccessUnitId(Guid id)
        {

            var command = new GetModulosByUnidadeAcessoIdQuery(id);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUnidadeAcesso")]
        public async Task<IActionResult> Get([FromQuery] GetUnidadeAcessoRequestFilter filter)
        {

            var command = new GetUnidadeAcessoQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("Search")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUnidadeAcesso")]
        public async Task<IActionResult> Search([FromQuery] GetUnidadeAcessoRequestFilter filter)
        {

            var command = new GetUnidadeAcessoByFilterQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUnidadeAcesso")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var command = new GetUnidadeAcessoByIdQuery(id);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost]
        [HttpResultActionFilter]
        [Authorize(Policy = "CadastrarUnidadeAcesso")]
        public async Task<IActionResult> Create([FromBody] CreateUnidadeAcessoRequest request)
        {

            var command = new CreateUnidadeAcessoCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPut("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AtualizarUnidadeAcesso")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUnidadeAcessoRequest request)
        {

            var command = new UpdateUnidadeAcessoCommand(id, request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost("Delete")]
        [HttpResultActionFilter]
        [Authorize(Policy = "DeletarUnidadeAcesso")]
        public async Task<IActionResult> Delete(
            [FromBody] DeleteUnidadeAcessoRequest request
        )
        {

            var command = new DeleteUnidadeAcessoCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

    }

}
