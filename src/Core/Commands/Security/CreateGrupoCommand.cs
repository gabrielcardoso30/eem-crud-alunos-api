using System;
using System.Collections.Generic;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security
{

    public class CreateGrupoCommand : IRequest<Result<GrupoResponse>>
    {

        public string Nome { get; set; }
        public Guid ModuloId { get; set; }
        public Guid EmpresaId { get; set; }
        public bool Padrao { get; set; } = false;
        public ICollection<string> Modulos { get; set; }
        public ICollection<string> UnidadesAcesso { get; set; }

        public CreateGrupoCommand(
            string nome, 
            Guid moduloId, 
            Guid empresaId, 
            bool padrao,
            ICollection<string> modulos,
            ICollection<string> unidadesAcesso
        )
        {
            Nome = nome;
            ModuloId = moduloId;
            EmpresaId = empresaId;
            Padrao = padrao;
            Modulos = modulos;
            UnidadesAcesso = unidadesAcesso;
        }

        public CreateGrupoCommand()
        {

        }

    }

}
