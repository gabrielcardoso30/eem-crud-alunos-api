using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Security;
using Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;

namespace Infra.Data.Repositories.Security
{
    public class ParametroSistemaRepository : BaseRepository<ParametroSistema>, IParametroSistemaRepository
    {
        private readonly AppDbContext _dbContext;

        public ParametroSistemaRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AsyncOutResult<IEnumerable<ParametroSistema>,int>> Get(Guid usuarioId, EnumTipoParametro? tipoParametro, int take, int offset, string sortingProp, bool asc)
        {
            var query = _dbContext.ParametroSistema
                .Where(p => p.Deletado == false).AsQueryable();

            if (usuarioId != Guid.Empty)
                query = query.Where(p => p.AspNetUsersId == usuarioId); 
            if (tipoParametro != null)
                query = query.Where(p => p.TipoParametro == tipoParametro); 

            if (DataHelpers.CheckExistingProperty<ParametroSistema>(sortingProp))
                query = query.OrderByDynamic(sortingProp, asc);

            return new AsyncOutResult<IEnumerable<ParametroSistema>, int>(await query.Skip(offset).Take(take).ToListAsync(), await query.CountAsync());

        }
        
        public virtual async Task<ParametroSistema> GetByIdWithUserId(Guid id, Guid usuarioId)
        {
            return await _dbContext.ParametroSistema
                .Where(w => w.AspNetUsersId == usuarioId && w.Id == id).FirstOrDefaultAsync();
        }
    }
}