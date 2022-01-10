using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{
    public class GetAuditoriaByIdQueryHandler : IRequestHandler<GetAuditoriaByIdQuery, Result<AuditoriaResponse>>
    {
        private readonly IAuditoriaRepository _repositoy;
        private readonly IMapper _mapper;
        public GetAuditoriaByIdQueryHandler(IAuditoriaRepository repositoy, IMapper mapper)
        {
            _repositoy = repositoy;
            _mapper = mapper;
        }
        public async Task<Result<AuditoriaResponse>> Handle(GetAuditoriaByIdQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<AuditoriaResponse>();

            var opcional = await _repositoy.GetById(query.Id);
            if (opcional == null)
            {
                result.WithNotFound("Auditoria não encontrada!");
                return result;
            }

            result.Value = _mapper.Map<AuditoriaResponse>(opcional);

            return result;
        }
    }
}
