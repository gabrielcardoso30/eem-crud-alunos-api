using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using Core.Interfaces.Repositories.Security;
using System;
using System.Linq;

namespace Infra.Data.Repositories.Security
{
    public class PermissaoRepository : BaseRepository<Permissao>, IPermissaoRepository
    {
        private readonly AppDbContext _dbContext;

        public PermissaoRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Permissao>> Get()
        {
            var query = _dbContext.Permissao
                .Include(x => x.PermissaoGrupo)
                .ThenInclude(x => x.Grupo);

            return await query.ToListAsync();
        }

        public async Task<IList<Permissao>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.Permissao.AsQueryable();

            if (ids != null) query = query.Where(gc => ids.Contains(gc.Id));

            return await query.ToListAsync();

        }
    }
}
