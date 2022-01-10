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

    public class DeleteResponsavelCommandHandler : IRequestHandler<DeleteResponsavelCommand, Result>
    {

        private readonly IResponsavelRepository _repository;

        public DeleteResponsavelCommandHandler(
            IResponsavelRepository repository
        )
        {
            _repository = repository;
        }

        public async Task<Result> Handle(DeleteResponsavelCommand request, CancellationToken cancellationToken)
        {

            var result = new Result();

            if (request.Request.Ids != null && request.Request.Ids?.Count > 0)
            {

                var registros = await _repository.Get(request.Request.Ids.ToArray());

                if (registros.Count == 0)
                {
                    result.WithNotFound("Registros não encontrados");
                    return result;
                }
                else
                {

                    await _repository.SoftDeleteRangeAsync(registros);

                }

            }
            else
            {

                result.WithError("Parâmetro inválido.");
                return result;

            }

            return result;

        }

    }

}
