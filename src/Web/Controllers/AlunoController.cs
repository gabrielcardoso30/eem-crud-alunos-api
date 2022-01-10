using Core.Commands.Gerencial;
using Core.Models.Filters;
using Core.Models.Responses.Gerencial;
using Core.Models.Requests.Gerencial;
using Core.Queries.Gerencial;
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
    public class AlunoController : BaseApiController
    {

        private readonly IMediator _mediator;

        public AlunoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarAluno")]
        public async Task<IActionResult> Get([FromQuery] GetAlunoRequestFilter filter)
        {

            var command = new GetAlunoQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("SearchBy/{filtro}/{valor}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarAluno")]
        public async Task<IActionResult> Search(string filtro, string valor, [FromQuery] GetAlunoRequestFilter filter)
        {
            
            IList<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>(){ new KeyValuePair<string, string>(filtro, valor) };
            filter.TableFilter = Newtonsoft.Json.JsonConvert.SerializeObject(keyValuePairs);
            var command = new GetAlunoQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarAluno")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var command = new GetAlunoByIdQuery(id);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost]
        [HttpResultActionFilter]
        [Authorize(Policy = "CadastrarAluno")]
        public async Task<IActionResult> Create([FromBody] CreateAlunoRequest request)
        {

            var command = new CreateAlunoCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPut("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AtualizarAluno")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAlunoRequest request)
        {

            var command = new UpdateAlunoCommand(id, request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost("Delete")]
        [HttpResultActionFilter]
        [Authorize(Policy = "DeletarAluno")]
        public async Task<IActionResult> Delete(
            [FromBody] DeleteAlunoRequest request
        )
        {

            var command = new DeleteAlunoCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

    }

}
