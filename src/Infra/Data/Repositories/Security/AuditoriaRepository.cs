using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infra.Data.Helpers;
using Microsoft.EntityFrameworkCore;
using Core.Entities.Security;
using Core.Enums;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;

namespace Infra.Data.Repositories.Security
{
    public class AuditoriaRepository : BaseRepository<Auditoria>, IAuditoriaRepository
    {
        private readonly AppDbContext _dbContext;

        public AuditoriaRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<AsyncOutResult<IEnumerable<Auditoria>,int>> Get(DateTime? dataEventoInicio ,DateTime? dataEventoFim , 
            EnumEntidade? entidade,  Guid usuarioId , EnumAcao? acao, string entidadeId, 
            int take, int offset, string sortingProp, bool asc)
        {
            var query = _dbContext.Auditoria
                .Include(i => i.AspNetUsers)
                .AsQueryable();
            
            if (dataEventoInicio != null)
                query = query.Where(p => p.DataEvento > dataEventoInicio);
            if (dataEventoFim != null)
                query = query.Where(p => p.DataEvento > dataEventoFim);
            if (usuarioId != Guid.Empty)
                query = query.Where(p => p.AspNetUsersId == usuarioId);
            if (acao != null)
                query = query.Where(p => p.EntityState == acao.ObterDescricaoEnum());
            if (!string.IsNullOrEmpty(entidadeId))
                query = query.Where(p => p.KeyValue == entidadeId);
            if (entidade != null)
                query = query.Where(p => p.Entidade == entidade.ObterDescricaoEnum());
            
            if (DataHelpers.CheckExistingProperty<Auditoria>(sortingProp))
                query = query.OrderByDynamic(sortingProp,asc);
            
            return new AsyncOutResult<IEnumerable<Auditoria>, int>(await query.Skip(offset).Take(take).ToListAsync(), await query.CountAsync());

        }
        
    }
}
