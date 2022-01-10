using AutoMapper;
using Core.Entities.Gerencial;
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

    public class GetResponsavelByIdQueryHandler : IRequestHandler<GetResponsavelByIdQuery, Result<ResponsavelResponse>>
    {

        private readonly IResponsavelRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
		private readonly IAlunoRepository _alunoRepository;

        public GetResponsavelByIdQueryHandler(
            IResponsavelRepository repository, 
            IMapper mapper,
            IAuthenticatedUser authenticatedUser,
			IAlunoRepository alunoRepository
        )
        {
			_alunoRepository = alunoRepository;
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<ResponsavelResponse>> Handle(GetResponsavelByIdQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<ResponsavelResponse>();

            var registro = await _repository.GetById(query.Id);
            if (registro == null)
            {
                result.WithNotFound("Registro não encontrado!");
                return result;
            }

            Aluno aluno = await _alunoRepository.GetById(registro.AlunoId);

            result.Value = _mapper.Map<ResponsavelResponse>(registro);
            result.Value.AlunoNome = aluno.Nome;

            return result;

        }

    }

}
