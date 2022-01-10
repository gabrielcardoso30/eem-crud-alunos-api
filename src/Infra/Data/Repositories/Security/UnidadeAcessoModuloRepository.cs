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

    public class UnidadeAcessoModuloRepository : BaseRepository<UnidadeAcessoModulo>, IUnidadeAcessoModuloRepository
    {

        private readonly AppDbContext _dbContext;

        public UnidadeAcessoModuloRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IList<UnidadeAcessoModulo>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.UnidadeAcessoModulo.AsQueryable();

            if (ids != null) query = query.Where(gc => ids.Contains(gc.Id));

            return await query.ToListAsync();

        }

        public async Task<IList<UnidadeAcessoModulo>> GetByUnidadesAcessoId(IEnumerable<Guid> ids)
        {

            var query = _dbContext.UnidadeAcessoModulo.AsQueryable().Where(gc => ids.Contains(gc.UnidadeAcessoId));
            return await query.ToListAsync();

        }

        public async Task<IList<UnidadeAcessoModulo>> GetByUnidadeAcessoId(Guid id)
        {

            var query = _dbContext.UnidadeAcessoModulo.AsQueryable().Where(gc => gc.UnidadeAcessoId == id);
            return await query.ToListAsync();

        }

    }

}
