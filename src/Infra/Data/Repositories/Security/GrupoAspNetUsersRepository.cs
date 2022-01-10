using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using Core.Interfaces.Repositories.Security;

namespace Infra.Data.Repositories.Security
{
    public class GrupoAspNetUsersRepository : BaseRepository<GrupoAspNetUsers>, IGrupoAspNetUsersRepository
    {
        private readonly AppDbContext _dbContext;

        public GrupoAspNetUsersRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public virtual async Task<IEnumerable<GrupoAspNetUsers>> GetGruposUsuario(Guid aspNetUsersId)
        {
            var query = _dbContext.GrupoAspNetUsers
                .Where(w => w.AspNetUsersId == aspNetUsersId)
                .AsQueryable(); 
            
            return await query.ToListAsync();
        }

        public async Task<bool> DeleteByGrupoId(Guid grupoId)
        {
            var grupos = _dbContext.GrupoAspNetUsers.Where(w => w.GrupoId == grupoId);
            _dbContext.RemoveRange(grupos);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
