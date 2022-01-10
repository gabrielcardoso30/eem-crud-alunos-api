using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Commands.Security;
using Core.Queries.Security;
using Microsoft.AspNetCore.Http;
using Web.Api;
using Web.Filters;
using System.Collections.ObjectModel;
using Core.Models.Filters;
using DinkToPdf.Contracts;
using Core.Helpers;
using Core.Interfaces.Services;
using Core.Models.Request.Security;

namespace Web.Controllers
{
    [Authorize]
    public class UsuarioController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IConverter _converter;
        private readonly IViewRenderService _viewRenderService;
        private readonly IMsSendMail _msSendMail;

        public UsuarioController(
            IMediator mediator,
            IViewRenderService viewRenderService,
            IConverter converter,
            IMsSendMail msSendMail)
        {
            _msSendMail = msSendMail;
            _converter = converter;
            _viewRenderService = viewRenderService;
            _mediator = mediator;
        }

        [HttpGet("{id}/permissoes")]
        [HttpResultActionFilter]
        public async Task<IActionResult> GetUsuarioPermissoes(Guid id)
        {

            var command = new GetUsuarioPermissoesQuery(id);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUsuario")]
        public async Task<IActionResult> Get()
        {

            var response = await _mediator.Send(new GetUsuarioQuery(null, "", "", ""));
            return Ok(response);

        }

        [HttpGet("Search")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUsuario")]
        public async Task<IActionResult> Search([FromQuery] BaseRequestFilter filter)
        {

            var command = new GetUsuarioByFilterQuery(filter);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpGet("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "ConsultarUsuario")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var command = new GetUsuarioByIdQuery(id);
            var response = await _mediator.Send(command);

            return Ok(response);
        }


        [HttpPut("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AtualizarUsuario")]
        public async Task<IActionResult> Update(
            Guid id,
            [FromBody] UpdateUsuarioCommand user
        )
        {

            user.Id = id;
            var response = await _mediator.Send(user);
            if (response?.Value == null || response?.HasError == true)
            {
                return Ok(response);
            }
            else
            {
                try
                {
                    string emailTemplate = await _viewRenderService.RenderToStringAsync("/Views/EmailTemplates/Security/DadosAcesso.cshtml", response.Value);
                    await _msSendMail.SendMailAsync(response.Value.Email, "Alteração dos Dados de Acesso", emailTemplate, "Usuário", response.Value.Id.ToString(), 0);
                    response.Value.Senha = null;
                    return Ok(response);
                }
                catch (System.Exception ex)
                {

                    return BadRequest("Usuário editado, contudo, não foi possível enviar o e-mail.");

                }
            }

        }

        [HttpPut("AlterarUnidadeAcesso")]
        [HttpResultActionFilter]
        //[Authorize(Policy = "AtualizarUsuario")]
        public async Task<IActionResult> ChangeUserAccessUnit(
            [FromBody] UpdateUsuarioUnidadeAcessoRequest request
        )
        {

            var command = new UpdateUsuarioUnidadeAcessoCommand(request);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost("{id}/atualizar-playerid")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AtualizarUsuario")]
        public async Task<IActionResult> AtualizarPlayerId(Guid id, [FromBody] string playerId)
        {
            var command = new UpdatePlayerIdCommand(id, playerId);
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "DeletarUsuario")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteUsuarioCommand(id, null);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("DeleteItems")]
        [HttpResultActionFilter]
        [Authorize(Policy = "DeletarUsuario")]
        public async Task<IActionResult> DeleteItems(
            [FromBody] DeleteUsuarioCommand command
        )
        {

            var response = await _mediator.Send(command);
            return Ok(response);

        }

        [HttpPost]
        [HttpResultActionFilter]
        [Authorize(Policy = "CadastrarUsuario")]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioCommand command)
        {
            var response = await _mediator.Send(command);
            if (response?.Value == null || response?.HasError == true)
            {
                return Ok(response);
            }
            else
            {
                try
                {
                    string emailTemplate = await _viewRenderService.RenderToStringAsync("/Views/EmailTemplates/Security/DadosAcesso.cshtml", response.Value);
                    await _msSendMail.SendMailAsync(response.Value.Email, "Dados de Acesso", emailTemplate, "Usuário", response.Value.Id.ToString(), 0);
                    response.Value.Senha = null;
                    return Ok(response);
                }
                catch (System.Exception ex)
                {

                    return BadRequest("Usuário criado, contudo, não foi possível enviar o e-mail.");

                }
            }
        }

        [HttpPost("{id}/grupo")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AssociarUsuarioGrupo")]
        public async Task<IActionResult> AddGrupoAspNetUsers(Guid id, [FromBody] CreateGrupoAspNetUsersCommand groupUser)
        {

            groupUser.Id = id;
            var response = await _mediator.Send(groupUser);
            return Ok(response);

        }

        [HttpPut("{id}/grupo")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AssociarUsuarioGrupo")]
        public async Task<IActionResult> UpdateGrupoAspNetUsers(Guid id, [FromBody] UpdateGrupoAspNetUsersCommand groupUser)
        {

            groupUser.Id = id;
            var response = await _mediator.Send(groupUser);
            return Ok(response);

        }

        [HttpDelete("grupo/{id}")]
        [HttpResultActionFilter]
        //[Authorize(Policy="ManterUsuario")]
        public async Task<IActionResult> DeleteGrupoAspNetUsers(Guid id)
        {
            var command = new DeleteGrupoAspNetUsersCommand(id);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("{id}/desbloquear")]
        [HttpResultActionFilter]
        [Authorize(Policy = "DesbloquearUsuario")]
        public async Task<IActionResult> DesbloquearUsuario(Guid id)
        {
            var command = new DesbloquearUsuarioCommand(id);
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPut("AlterarStatus/{id}")]
        [HttpResultActionFilter]
        [Authorize(Policy = "AtualizarUsuario")]
        public async Task<IActionResult> AlterarStatus(
            Guid id
        )
        {

            var command = new UpdateUsuarioStatusCommand(id);
            var response = await _mediator.Send(command);
            return Ok(response);

        }

    }
}