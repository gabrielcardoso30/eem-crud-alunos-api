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
    public class GetGruposQueryHandler : IRequestHandler<GetGruposQuery, Result<IEnumerable<GrupoResponse>>>
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IMapper _mapper;
        public GetGruposQueryHandler(IGrupoRepository grupoRepository, IMapper mapper)
        {
            _grupoRepository = grupoRepository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<GrupoResponse>>> Handle(GetGruposQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<GrupoResponse>>();
                
            var grupos = await _grupoRepository.Get(
                query.Take, query.Skip, 
                query.SortingProp, query.Ascending);

            result.Value = grupos.Result(out var count).Select(p => _mapper.Map<GrupoResponse>(p));
            result.Count = count;
                
            return result;
        }
    }
}
