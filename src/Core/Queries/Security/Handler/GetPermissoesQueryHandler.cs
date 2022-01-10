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
    public class GetPermissoesQueryHandler : IRequestHandler<GetPermissoesQuery, Result<IEnumerable<PermissaoResponse>>>
    {
        private readonly IPermissaoRepository _repository;
        private readonly IMapper _mapper;
        
        public GetPermissoesQueryHandler(IPermissaoRepository repository, 
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<IEnumerable<PermissaoResponse>>> Handle(GetPermissoesQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<IEnumerable<PermissaoResponse>>();
                
            var permissoes = (await _repository.Get()).ToList();
                
            result.Value = permissoes.Select(p => _mapper.Map<PermissaoResponse>(p));

            return result;
        }
    }
}
