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
    public class ResponsavelController : BaseApiController
    {

        private readonly IMediator _mediator;

        public ResponsavelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarResponsavel")]
        public async Task<IActionResult> Get([FromQuery] GetResponsavelRequestFilter filter)
        {

            var command = new GetResponsavelQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("SearchBy/{filtro}/{valor}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarResponsavel")]
        public async Task<IActionResult> Search(string filtro, string valor, [FromQuery] GetResponsavelRequestFilter filter)
        {
            
            IList<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>(){ new KeyValuePair<string, string>(filtro, valor) };
            filter.TableFilter = Newtonsoft.Json.JsonConvert.SerializeObject(keyValuePairs);
            var command = new GetResponsavelQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarResponsavel")]
        public async Task<IActionResult> GetById(Guid id)
        {

            var command = new GetResponsavelByIdQuery(id);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost]
        [HttpResultActionFilter]
        [Authorize(Policy = "CadastrarResponsavel")]
        public async Task<IActionResult> Create([FromBody] CreateResponsavelRequest request)
        {

            var command = new CreateResponsavelCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPut("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AtualizarResponsavel")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResponsavelRequest request)
        {

            var command = new UpdateResponsavelCommand(id, request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost("Delete")]
        [HttpResultActionFilter]
        [Authorize(Policy = "DeletarResponsavel")]
        public async Task<IActionResult> Delete(
            [FromBody] DeleteResponsavelRequest request
        )
        {

            var command = new DeleteResponsavelCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

    }

}
