using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, Result>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public DeleteUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public async Task<Result> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {

            var result = new Result();

            if (request.Ids != null && request.Ids?.Count > 0)
            {

                var usuarios = await _usuarioRepository.Get(request.Ids.ToArray());

                if (usuarios.Count == 0)
                {
                    result.WithNotFound("Usuarios não encontrados");
                    return result;
                }
                else
                {

                    if (usuarios.Where(gc => gc.UserName == "admin").FirstOrDefault() != null)
                    {
                        result.WithError("O usuário administrador do sistema não pode ser excluído!");
                        return result;
                    }

                    foreach (var usuario in usuarios)
                    {

                        // if (usuario.Deletado)
                        //     await _usuarioRepository.Deactivated(usuario, false);
                        // else
                        //     await _usuarioRepository.Deactivated(usuario, true);
                        if (usuario.Ativo) usuario.Ativo = false;
                        else usuario.Ativo = true;
                        await _usuarioRepository.UpdateAsync(usuario);

                    }
                }

            }
            else
            {

                var usuario = await _usuarioRepository.GetById(request.Id);
                if (usuario == null)
                {
                    result.WithNotFound("Usuario não encontrado");
                    return result;
                }

                if (usuario.UserName == "admin")
                {
                    result.WithError("O usuário administrador do sistema não pode ser excluído!");
                    return result;
                }

                // if (usuario.Deletado)
                //     await _usuarioRepository.Deactivated(usuario, false);
                // else
                //     await _usuarioRepository.Deactivated(usuario, true);
                if (usuario.Ativo) usuario.Ativo = false;
                else usuario.Ativo = true;
                await _usuarioRepository.UpdateAsync(usuario);

            }

            return result;

        }
    }
}
