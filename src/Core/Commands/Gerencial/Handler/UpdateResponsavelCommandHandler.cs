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
        private readonly IMapper _mapper;

        public UpdateResponsavelCommandHandler(IResponsavelRepository repository, IMapper mapper)
        {
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

            var registerForUpdate = _mapper.Map(request.Request, oldRegister);
            if (await _repository.UpdateAsync(oldRegister))
                result.Value = _mapper.Map<ResponsavelResponse>(registerForUpdate);

            return result;

        }

    }

}
