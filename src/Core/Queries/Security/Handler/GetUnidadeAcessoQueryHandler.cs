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

    public class GetUnidadeAcessoQueryHandler : IRequestHandler<GetUnidadeAcessoQuery, Result<IEnumerable<UnidadeAcessoResponse>>>
    {

        private readonly IUnidadeAcessoRepository _repository;
        private readonly IMapper _mapper;

        public GetUnidadeAcessoQueryHandler(
            IUnidadeAcessoRepository repository,
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UnidadeAcessoResponse>>> Handle(GetUnidadeAcessoQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<UnidadeAcessoResponse>>();
            var registros = await _repository.GetAll(query.Filter.Take, query.Filter.Skip, query.Filter.SortingProp, query.Filter.Ascending);
            IList<UnidadeAcessoResponse> enumerable = registros.Result(out var count).Select(p => _mapper.Map<UnidadeAcessoResponse>(p)).ToList();

            enumerable = enumerable.OrderBy(x => x.Nome).ToArray();
            result.Value = enumerable;
            result.Count = count;
            result.TableCount = await _repository.CountTable();

            return result;

        }

    }

}
