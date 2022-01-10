using System;
using System.Threading.Tasks;
using Core.Commands.Security;
using Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Queries.Security;
using Web.Api;
using Web.Filters;

namespace Web.Controllers
{
    [Authorize]
    public class ParametroSistemaController : BaseApiController
    {
        private readonly IMediator _mediator;

        public ParametroSistemaController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        //[Authorize(Policy ="ConsultarParametroSistema")]
        [HttpResultActionFilter]
        public async Task<IActionResult> Get([FromQuery] GetParametroSistemaQuery query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        [HttpResultActionFilter]
        //[Authorize(Policy ="ConsultarParametroSistema")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetParametroSistemaByIdQuery(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        [HttpResultActionFilter]
        //[Authorize(Policy ="AtualizarParametroSistema")]
        public async Task<IActionResult> Update(Guid id, 
            [FromBody] EnumTipoParametro? tipoParametro,
            [FromBody] EnumTipoValorParametro? tipoValor,
            [FromBody] bool? valorBit,
            [FromBody] string valorTexto)
        {
            var command = new UpdateParametroSistemaCommand(id, tipoParametro, tipoValor,valorBit,valorTexto);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        
        [HttpDelete("{id}")]
        [HttpResultActionFilter]
        //[Authorize(Policy ="DeletarParametroSistema")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteParametroSistemaCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);
        }
        [HttpPost]
        [HttpResultActionFilter]
        //[Authorize(Policy ="CadastrarParametroSistema")]
        public async Task<IActionResult> Create([FromBody] CreateParametroSistemaCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }
    }
}