using System;
using System.Collections.Generic;
using Core.Enums;
using Core.Helpers;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security
{
    public class GetAuditoriaQuery : BaseQueryFilter, IRequest<Result<IEnumerable<AuditoriaResponse>>>
    {
        public DateTime? DataEventoInicio { get; set; }
        public DateTime? DataEventoFim { get; set; }
        public EnumEntidade? Entidade { get; set; }
        public string EntidadeId { get; set; }
        public Guid UsuarioId { get; set; }
        public EnumAcao? Acao { get; set; }

        public GetAuditoriaQuery(
            DateTime? dataEventoInicio,
            DateTime? dataEventoFim,
            EnumEntidade? entidade,
            string entidadeId,
            Guid usuarioId,
            EnumAcao? acao)
        {
            DataEventoInicio = dataEventoInicio;
            DataEventoFim = dataEventoFim;
            Entidade = entidade;
            EntidadeId = entidadeId;
            UsuarioId = usuarioId;
            Acao = acao;
        }
    }
}