using AutoMapper;
using Core.Helpers;
using Core.Interfaces.Repositories.Gerencial;
using Core.Interfaces.Security;
using Core.Models.Responses.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Gerencial.Handler
{

    public class GetAlunoByIdQueryHandler : IRequestHandler<GetAlunoByIdQuery, Result<AlunoResponse>>
    {

        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;

        public GetAlunoByIdQueryHandler(
            IAlunoRepository repository, 
            IMapper mapper,
            IAuthenticatedUser authenticatedUser
        )
        {
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<AlunoResponse>> Handle(GetAlunoByIdQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<AlunoResponse>();

            var registro = await _repository.GetById(query.Id);
            if (registro == null)
            {
                result.WithNotFound("Registro não encontrado!");
                return result;
            }

            result.Value = _mapper.Map<AlunoResponse>(registro);

            return result;

        }

    }

}
