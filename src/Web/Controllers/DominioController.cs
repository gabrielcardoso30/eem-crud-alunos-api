using MediatR;
using Microsoft.AspNetCore.Mvc;
using Core.Helpers;
using Web.Api;

#pragma warning disable 1998

namespace Web.Controllers
{
    //[Authorize]
    public class DominioController : BaseApiController
    {
        private readonly IMediator _mediator;

        public DominioController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet("enums")]
        public IActionResult GetAllEnums()
        {
            var enums = EnumHelper.GetAllEnums();

            return Json(new
            {
                Results = enums
            });
        }

        [HttpGet("enums/{name}")]
        public IActionResult GetEnumDescricao(string name)
        {
            var dados = EnumHelper.GetValueEnum(name);

            return Json(new
            {
                Results = dados
            });
        }
        
    }
}