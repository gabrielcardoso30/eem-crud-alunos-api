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

    public class UpdateAlunoCommandHandler : IRequestHandler<UpdateAlunoCommand, Result<AlunoResponse>>
    {

        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;

        public UpdateAlunoCommandHandler(IAlunoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<AlunoResponse>> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<AlunoResponse>();
            var oldRegister = await _repository.GetById(request.Id);

            if (oldRegister == null)
            {
                result.WithNotFound("Registro não encontrado!");
                return result;
            }

            if (String.IsNullOrEmpty(request.Request.Nome)
                || String.IsNullOrEmpty(request.Request.Segmento)
                || request.Request.DataNascimento < DateTime.Parse("1900-01-01")
            )
            {
                result.WithError("Nome, segmento ou a data de nascimento estão inválidos!");
                return result;
            }
            
            var registerForUpdate = _mapper.Map(request.Request, oldRegister);
            if (await _repository.UpdateAsync(oldRegister))
                result.Value = _mapper.Map<AlunoResponse>(registerForUpdate);

            return result;

        }

    }

}
