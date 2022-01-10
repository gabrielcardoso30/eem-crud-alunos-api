using AutoMapper;
using Core.Entities.Gerencial;
using Core.Helpers;
using Core.Interfaces.Repositories.Gerencial;
using Core.Models.Responses.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Commands.Gerencial.Handler
{

    public class CreateAlunoCommandHandler : IRequestHandler<CreateAlunoCommand, Result<AlunoResponse>>
    {

        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;

        public CreateAlunoCommandHandler(IAlunoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<AlunoResponse>> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<AlunoResponse>();

            if (String.IsNullOrEmpty(request.Request.Nome) 
                || String.IsNullOrEmpty(request.Request.Segmento)
                || request.Request.DataNascimento < DateTime.Parse("1900-01-01")
            )
            {
                result.WithError("Nome, sigla ou a data de nascimento estão inválidos!");
                return result;
            }

            var registro = _mapper.Map<Aluno>(request.Request);
            registro.UnidadeAcessoId = await _repository.GetSelectedAccessUnitIdAsync();
            var grupo = await _repository.AddAsync(registro);
            result.Value = _mapper.Map<AlunoResponse>(grupo);
            return result;

        }

    }

}
