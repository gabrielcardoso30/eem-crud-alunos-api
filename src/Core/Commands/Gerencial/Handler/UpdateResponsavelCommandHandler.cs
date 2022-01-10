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

    public class UpdateResponsavelCommandHandler : IRequestHandler<UpdateResponsavelCommand, Result<ResponsavelResponse>>
    {

        private readonly IResponsavelRepository _repository;
		private readonly IAlunoRepository _alunoRepository;
        private readonly IMapper _mapper;

		public UpdateResponsavelCommandHandler(
			IResponsavelRepository repository,
			IMapper mapper,
			IAlunoRepository alunoRepository)
        {
			_alunoRepository = alunoRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ResponsavelResponse>> Handle(UpdateResponsavelCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<ResponsavelResponse>();
            var oldRegister = await _repository.GetById(request.Id);

            if (oldRegister == null)
            {
                result.WithNotFound("Registro não encontrado!");
                return result;
            }

            if (String.IsNullOrEmpty(request.Request.Nome)
                 || String.IsNullOrEmpty(request.Request.Parentesco)
                 || String.IsNullOrEmpty(request.Request.Telefone)
                 || request.Request.DataNascimento < DateTime.Parse("1900-01-01")
            )
            {
                result.WithError("Nome, parentesco, data de nascimento ou telefone estão inválidos!");
                return result;
            }

            if (request.Request.AlunoId == null || request.Request.AlunoId == Guid.Empty)
            {
                result.WithError("Um responsável precisa ter um aluno selecionado.");
                return result;
            }
            else
            {
                Aluno aluno = await _alunoRepository.GetById(request.Request.AlunoId ?? Guid.Empty);
                if (aluno == null)
                {
                    result.WithError("Aluno informado não existe ou está inativo.");
                    return result;
                }
            }

            if (String.IsNullOrEmpty(request.Request.Email))
            {
                result.WithError("O e-mail é obrigatório!");
                return result;
            }

            var registerForUpdate = _mapper.Map(request.Request, oldRegister);
            if (await _repository.UpdateAsync(oldRegister))
                result.Value = _mapper.Map<ResponsavelResponse>(registerForUpdate);

            return result;

        }

    }

}
