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

    public class GetUsuarioQueryHandler : IRequestHandler<GetUsuarioQuery, Result<IEnumerable<UsuarioResponse>>>
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public GetUsuarioQueryHandler(
            IUsuarioRepository usuarioRepository, 
            IMapper mapper
        )
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<UsuarioResponse>>> Handle(GetUsuarioQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<UsuarioResponse>>();
            var registros = await _usuarioRepository.Get(
                query.GrupoId, 
                query.Nome, 
                query.Login, 
                query.Cpf,
                query.Take, 
                query.Skip, 
                query.SortingProp, 
                query.Ascending);
                
            result.Value = registros.Result(out var count).Select(p => _mapper.Map<UsuarioResponse>(p));
            result.Count = count;
            result.TableCount = await _usuarioRepository.CountTable();

            return result;

        }

    }

}
