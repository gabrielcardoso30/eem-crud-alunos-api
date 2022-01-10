using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{
    public class GetParametroSistemaQueryHandler : IRequestHandler<GetParametroSistemaQuery, Result<IEnumerable<ParametroSistemaResponse>>>
    {
        private readonly IParametroSistemaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IUsuarioRepository _usuarioRepository;
        public GetParametroSistemaQueryHandler(IParametroSistemaRepository repository, IMapper mapper,IAuthenticatedUser authenticatedUser,
            IUsuarioRepository usuarioRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Result<IEnumerable<ParametroSistemaResponse>>> Handle(GetParametroSistemaQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<ParametroSistemaResponse>>();
                
            var usuarioLogado = await _usuarioRepository.GetById(_authenticatedUser.GuidLogin());
                
            var parametroSistemas = await _repository.Get(usuarioLogado.Id, query.TipoParametro, query.Take, query.Skip, 
                query.SortingProp, query.Ascending);
                
            result.Value = parametroSistemas.Result(out var count).Select(p => _mapper.Map<ParametroSistemaResponse>(p));
            result.Count = count;
                
            return result;
        }
    }
}
