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
    public class GetParametroSistemaByIdQueryHandler : IRequestHandler<GetParametroSistemaByIdQuery, Result<ParametroSistemaResponse>>
    {
        private readonly IParametroSistemaRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
        public GetParametroSistemaByIdQueryHandler(IParametroSistemaRepository repository, IMapper mapper, IAuthenticatedUser authenticatedUser)
        {
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }
        public async Task<Result<ParametroSistemaResponse>> Handle(GetParametroSistemaByIdQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<ParametroSistemaResponse>();
                
            var ParametroSistema = await _repository.GetByIdWithUserId(query.Id, _authenticatedUser.GuidLogin());
            if (ParametroSistema == null)
            {
                result.WithNotFound("ParametroSistema não encontrado!");
                return result;
            }

            result.Value = _mapper.Map<ParametroSistemaResponse>(ParametroSistema);

            return result;
        }
    }
}
