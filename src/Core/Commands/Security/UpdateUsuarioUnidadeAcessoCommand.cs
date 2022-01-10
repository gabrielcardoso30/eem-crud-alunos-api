using System;
using Core.Helpers;
using Core.Models.Request.Security;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Commands.Security
{

    public class UpdateUsuarioUnidadeAcessoCommand : IRequest<Result<UsuarioResponse>>
    {

        public Guid Id { get; set; }
        public UpdateUsuarioUnidadeAcessoRequest Request { get; set; }

        public UpdateUsuarioUnidadeAcessoCommand(
            Guid id,
            UpdateUsuarioUnidadeAcessoRequest request
        )
        {
            Id = id;
            Request = request;
        }

        public UpdateUsuarioUnidadeAcessoCommand(
            UpdateUsuarioUnidadeAcessoRequest request
        )
        {
            Request = request;
        }

    }

}