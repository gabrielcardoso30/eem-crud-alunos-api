using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using Core.Interfaces.Repositories.Security;

namespace Infra.Data.Repositories.Security
{
    public class PermissaoGrupoRepository : BaseRepository<PermissaoGrupo>, IPermissaoGrupoRepository
    {
        private readonly AppDbContext _dbContext;

        public PermissaoGrupoRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<PermissaoGrupo>> Get(Guid grupoId)
        {
            var query = _dbContext.PermissaoGrupo.Include(i => i.Permissao).AsQueryable();

            if (grupoId != Guid.Empty)
                query = query.Where(p => p.GrupoId == grupoId);

            return await query.ToListAsync();
        }

        public async Task<IList<PermissaoGrupo>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.PermissaoGrupo.AsQueryable();

            if (ids != null) query = query.Where(gc => ids.Contains(gc.GrupoId));

            return await query.ToListAsync();

        }

    }
}
