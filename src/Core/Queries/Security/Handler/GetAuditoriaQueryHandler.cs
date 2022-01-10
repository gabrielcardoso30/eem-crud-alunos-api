using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{
    public class GetAuditoriaQueryHandler : IRequestHandler<GetAuditoriaQuery, Result<IEnumerable<AuditoriaResponse>>>
    {
        private readonly IAuditoriaRepository _repositoy;
        private readonly IMapper _mapper;
        public GetAuditoriaQueryHandler(IAuditoriaRepository repositoy, IMapper mapper)
        {
            _repositoy = repositoy;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<AuditoriaResponse>>> Handle(GetAuditoriaQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<AuditoriaResponse>>();

            var auditoria = await _repositoy.Get(query.DataEventoInicio, query.DataEventoFim,
                query.Entidade, query.UsuarioId,
                query.Acao, query.EntidadeId,
                query.Take, query.Skip,
                query.SortingProp, query.Ascending);

            result.Value = auditoria.Result(out var count).Select(p => _mapper.Map<AuditoriaResponse>(p));
            result.Count = count;

            return result;
        }
    }
}
