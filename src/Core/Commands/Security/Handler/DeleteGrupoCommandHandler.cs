using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{

    public class DeleteGrupoCommandHandler : IRequestHandler<DeleteGrupoCommand, Result>
    {

        private readonly IGrupoRepository _repositoy;
        private readonly IGrupoAspNetUsersRepository _grupoAspNetUsersRepository;
        public DeleteGrupoCommandHandler(IGrupoRepository repositoy,
            IGrupoAspNetUsersRepository grupoAspNetUsersRepository)
        {
            _repositoy = repositoy;
            _grupoAspNetUsersRepository = grupoAspNetUsersRepository;
        }
        public async Task<Result> Handle(DeleteGrupoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result();

            if (request.Ids != null && request.Ids?.Count > 0)
            {

                var perfis = await _repositoy.Get(request.Ids.ToArray());

                if (perfis.Count == 0)
                {
                    result.WithNotFound("Grupos não encontrados");
                    return result;
                }
                else
                {

                    if (perfis.Where(gc => gc.Nome == "Administrador").FirstOrDefault() != null)
                    {
                        result.WithError("O perfil administrador do sistema não pode ser excluído!");
                        return result;
                    }

                    foreach (var perfil in perfis) await _repositoy.Deactivated(perfil);
                }

            }
            else
            {

                var grupo = await _repositoy.GetById(request.Id);
                if (grupo == null)
                {
                    result.WithNotFound("Grupo não encontrado");
                    return result;
                }

                if (grupo.Nome == "Administrador")
                {
                    result.WithError("O perfil administrador do sistema não pode ser excluído!");
                    return result;
                }

                await _repositoy.Deactivated(grupo);
                await _grupoAspNetUsersRepository.DeleteByGrupoId(grupo.Id);

            }            
                
            return result;

        }

    }

}
