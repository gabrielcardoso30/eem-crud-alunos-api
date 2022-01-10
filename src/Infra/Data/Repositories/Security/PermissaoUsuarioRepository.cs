using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using Core.Interfaces.Repositories.Security;

namespace Infra.Data.Repositories.Security
{
    public class PermissaoUsuarioRepository : BaseRepository<PermissaoUsuario>, IPermissaoUsuarioRepository
    {
        private readonly AppDbContext _dbContext;

        public PermissaoUsuarioRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<PermissaoUsuario>> Get(Guid usuarioId)
        {
            var query = _dbContext.PermissaoUsuario
                                    .Include(p => p.Permissao)
                                    .AsQueryable();

            if (usuarioId != Guid.Empty)
                query = query.Where(p => p.AspNetUsersId == usuarioId);

            return await query.ToListAsync();
        }
        
    }
}
