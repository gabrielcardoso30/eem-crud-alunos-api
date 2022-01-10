using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Commands.Security
{

    public class UpdateGrupoCommand : IRequest<Result<GrupoResponse>>
    {

        public Guid Id { get; set; }
        public string Nome { get; set; }
        //public Guid ModuloId { get; set; }
        public bool Padrao { get; set; }
        public ICollection<string> Modulos { get; set; }
        public ICollection<string> UnidadesAcesso { get; set; }

        public UpdateGrupoCommand(
            Guid id, 
            string nome, 
            //Guid moduloId, 
            bool padrao,
            ICollection<string> modulos,
            ICollection<string> unidadesAcesso
        )
        {
            Id = id;
            Nome = nome;
            //ModuloId = moduloId;
            Padrao = padrao;
            Modulos = modulos;
            UnidadesAcesso = unidadesAcesso;
        }

        public UpdateGrupoCommand()
        {

        }

    }

}
