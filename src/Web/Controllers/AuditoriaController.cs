using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Queries.Security;
using Web.Api;
using Web.Filters;

namespace Web.Controllers
{
    [Authorize]
    public class AuditoriaController : BaseApiController
    {
        private readonly IMediator _mediator;

        public AuditoriaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [HttpResultActionFilter]
        public async Task<IActionResult> Get([FromQuery] GetAuditoriaQuery query)
        {
            var response = await _mediator.Send(query);

            return Ok(response);
        }

        [HttpGet("{id}")]
        [HttpResultActionFilter]
        public async Task<IActionResult> GetById(Guid id)
        {
            var query = new GetAuditoriaByIdQuery(id);
            var response = await _mediator.Send(query);

            return Ok(response);
        }

    }
}