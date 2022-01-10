using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repositories.Security
{

    public class GrupoUnidadeAcessoRepository : BaseRepository<GrupoUnidadeAcesso>, IGrupoUnidadeAcessoRepository
    {

        private readonly AppDbContext _dbContext;

        public GrupoUnidadeAcessoRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IList<GrupoUnidadeAcesso>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.GrupoUnidadeAcesso.AsQueryable();

            if (ids != null) query = query.Where(gc => ids.Contains(gc.Id));

            return await query.ToListAsync();

        }

        public async Task<IList<GrupoUnidadeAcesso>> GetByGruposId(IEnumerable<Guid> ids)
        {

            var query = _dbContext.GrupoUnidadeAcesso.AsQueryable().Where(gc => ids.Contains(gc.GrupoId));
            return await query.ToListAsync();

        }

        public async Task<IList<GrupoUnidadeAcesso>> GetByGrupoId(Guid id)
        {

            var query = _dbContext.GrupoUnidadeAcesso.AsQueryable().Where(gc => gc.GrupoId == id);
            return await query.ToListAsync();

        }

    }

}
