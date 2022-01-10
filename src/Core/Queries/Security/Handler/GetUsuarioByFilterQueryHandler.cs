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

    public class GetUsuarioByFilterQueryHandler : IRequestHandler<GetUsuarioByFilterQuery, Result<IEnumerable<UsuarioResponse>>>
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GetUsuarioByFilterQueryHandler(
            IUsuarioRepository usuarioRepository, 
            IMapper mapper
        )
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UsuarioResponse>>> Handle(GetUsuarioByFilterQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<UsuarioResponse>>();
            var registros = await _usuarioRepository.SearchAll(query.Filter.Text, query.Filter.Take, query.Filter.Skip);
            IList<UsuarioResponse> enumerable = registros.Result(out var count).Select(p => _mapper.Map<UsuarioResponse>(p)).ToList();

            enumerable = enumerable.OrderBy(x => x.Nome).ToArray();
            result.Value = enumerable;
            result.Count = count;
            result.TableCount = await _usuarioRepository.CountTable();

            return result;

        }

    }

}
