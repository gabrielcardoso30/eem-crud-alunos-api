using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities.Security;

namespace Core.Interfaces.Repositories.Security
{
    public interface IPermissaoUsuarioRepository : IBaseRepository<PermissaoUsuario>
    {
        Task<IEnumerable<PermissaoUsuario>> Get(Guid usuarioId);

    }
}
