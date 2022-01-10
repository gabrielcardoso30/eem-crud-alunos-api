using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class CreateGrupoAspNetUsersCommandHandler : IRequestHandler<CreateGrupoAspNetUsersCommand, Result<List<GrupoAspNetUsersResponse>>>
    {
        private readonly IGrupoAspNetUsersRepository _repository;
        private readonly IMapper _mapper;
        public CreateGrupoAspNetUsersCommandHandler(IGrupoAspNetUsersRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<Result<List<GrupoAspNetUsersResponse>>> Handle(CreateGrupoAspNetUsersCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<List<GrupoAspNetUsersResponse>>();
            var lista = new List<GrupoAspNetUsersResponse>();

            foreach (var item in request.GrupoId)
            {
                var grupoAspNetUsersNew = new GrupoAspNetUsers()
                {
                    GrupoId = item,
                    AspNetUsersId = request.Id
                };
                
                var grupoAspNetUsers = await _repository.AddAsync(grupoAspNetUsersNew);
                lista.Add(_mapper.Map<GrupoAspNetUsersResponse>(grupoAspNetUsers));
            }

            result.Value = lista;

            return result;
        }
    }
}
