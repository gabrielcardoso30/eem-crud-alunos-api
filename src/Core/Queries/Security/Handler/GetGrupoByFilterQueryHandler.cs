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

    public class GetGrupoByFilterQueryHandler : IRequestHandler<GetGrupoByFilterQuery, Result<IEnumerable<GrupoResponse>>>
    {

        private readonly IGrupoRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GetGrupoByFilterQueryHandler(
            IGrupoRepository usuarioRepository, 
            IMapper mapper
        )
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<GrupoResponse>>> Handle(GetGrupoByFilterQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<GrupoResponse>>();
            var registros = await _usuarioRepository.SearchAll(query.Filter.Text, query.Filter.Take, query.Filter.Skip);
            IList<GrupoResponse> enumerable = registros.Result(out var count).Select(p => _mapper.Map<GrupoResponse>(p)).ToList();

            enumerable = enumerable.OrderBy(x => x.Nome).ToArray();
            result.Value = enumerable;
            result.Count = count;
            result.TableCount = await _usuarioRepository.CountTable();

            return result;

        }

    }

}
