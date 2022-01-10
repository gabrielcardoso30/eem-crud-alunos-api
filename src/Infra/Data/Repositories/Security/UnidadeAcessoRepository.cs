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

    public class UnidadeAcessoRepository : BaseRepository<UnidadeAcesso>, IUnidadeAcessoRepository
    {

        private readonly AppDbContext _dbContext;

        public UnidadeAcessoRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IList<UnidadeAcesso>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.UnidadeAcesso.AsQueryable();

            if (ids != null) query = query.Where(gc => ids.Contains(gc.Id));

            return await query.ToListAsync();

        }

        public async Task<UnidadeAcesso> GetUnidadeAcessoParaTestes()
        {

            var query = _dbContext.UnidadeAcesso.AsQueryable().Where(gc => gc.Id == Guid.Parse("a89ee7c9-01c5-4387-95ab-dfef58eac490"));
            return await query.FirstOrDefaultAsync();

        }

        public async Task<IList<UnidadeAcesso>> ListByStatus(bool ativo)
        {

            var query = _dbContext.UnidadeAcesso.AsQueryable().Where(gc => gc.Ativo == ativo);
            return await query.ToListAsync();

        }

        public async Task<IList<UnidadeAcesso>> Search(string text, int? take, int? offSet)
        {

            var query = _dbContext.UnidadeAcesso.AsQueryable();
            query = query.Where(gc => gc.Nome.Contains(text));

            if (take != null && offSet != null) return await query.Skip((int)offSet).Take((int)take).ToListAsync();
            else return await query.ToListAsync();

        }

        public async Task<AsyncOutResult<IEnumerable<UnidadeAcesso>, int>> SearchAll(string text, int? take, int? offSet)
        {

            var query = _dbContext.UnidadeAcesso.AsQueryable();
            query = query.Where(gc => gc.Nome.Contains(text));
            int totalCount = await query.CountAsync();

            if (take != null && offSet != null) 
                return new AsyncOutResult<IEnumerable<UnidadeAcesso>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), totalCount);
            else 
                return new AsyncOutResult<IEnumerable<UnidadeAcesso>, int>(await query.ToListAsync(), totalCount);

        }

    }

}
