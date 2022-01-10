using System;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Commands.Security
{
    public class UpdateUsuarioStatusCommand : IRequest<Result<UsuarioResponse>>
    {
        public Guid Id;
        //public bool Ativo { get; set; }

        public UpdateUsuarioStatusCommand(
            Guid id
            //bool ativo
        )
        {
            Id = id;
            //Ativo = ativo;
        }

        public UpdateUsuarioStatusCommand()
        {

        }

    }
}