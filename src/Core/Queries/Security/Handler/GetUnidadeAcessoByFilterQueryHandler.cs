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

    public class GetUnidadeAcessoByFilterQueryHandler : IRequestHandler<GetUnidadeAcessoByFilterQuery, Result<IEnumerable<UnidadeAcessoResponse>>>
    {

        private readonly IUnidadeAcessoRepository _unidadeAcessoRepository;
        private readonly IMapper _mapper;

        public GetUnidadeAcessoByFilterQueryHandler(
            IUnidadeAcessoRepository unidadeAcessoRepository, 
            IMapper mapper
        )
        {
            _unidadeAcessoRepository = unidadeAcessoRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UnidadeAcessoResponse>>> Handle(GetUnidadeAcessoByFilterQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<UnidadeAcessoResponse>>();
            var registros = await _unidadeAcessoRepository.SearchAll(query.Filter.Text, query.Filter.Take, query.Filter.Skip);
            IList<UnidadeAcessoResponse> enumerable = registros.Result(out var count).Select(p => _mapper.Map<UnidadeAcessoResponse>(p)).ToList();

            enumerable = enumerable.OrderBy(x => x.Nome).ToArray();
            result.Value = enumerable;
            result.Count = count;
            result.TableCount = await _unidadeAcessoRepository.CountTable();

            return result;

        }

    }

}
