using Core.Entities.Gerencial;
using Core.Helpers;
using Core.Interfaces.Repositories.Gerencial;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repositories.Gerencial
{

    public class ResponsavelRepository : BaseRepository<Responsavel>, IResponsavelRepository
    {

        private readonly AppDbContext _dbContext;
        private readonly Guid _unidadeAcessoSelecionada;

        public ResponsavelRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _unidadeAcessoSelecionada = GetSelectedAccessUnitId();
        }

        public async Task<IList<Responsavel>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.Responsavel.AsQueryable();

            if (ids != null) query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada && ids.Contains(gc.Id));

            return await query.ToListAsync();

        }

        public async Task<IList<Responsavel>> GetByAlunoId(Guid id)
        {

            var query = _dbContext.Responsavel.AsQueryable().Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada && gc.AlunoId == id);
            return await query.ToListAsync();

        }

        public async Task<IList<Responsavel>> GetByAlunosId(IEnumerable<Guid> ids)
        {

            var query = _dbContext.Responsavel.AsQueryable().Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada && ids.Contains(gc.AlunoId));
            return await query.ToListAsync();

        }

        public async Task<IList<Responsavel>> Search(string text, int? take, int? offSet)
        {

            var query = _dbContext.Responsavel.AsQueryable();
            if (!String.IsNullOrEmpty(text)) query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada && 
            (
                gc.Nome.ToUpper().Contains(text.ToUpper())
                || gc.Parentesco.ToUpper().Contains(text.ToUpper())
                || gc.Telefone.ToUpper().Contains(text.ToUpper())
                || (gc.Email ?? String.Empty).ToUpper().Contains(text.ToUpper())
            ));

            if (take != null && offSet != null) return await query.Skip((int)offSet).Take((int)take).ToListAsync();
            else return await query.ToListAsync();

        }

        public async Task<AsyncOutResult<IEnumerable<Responsavel>, int>> SearchAll(string text, int? take, int? offSet, string tableFilter)
        {

            var query = _dbContext.Responsavel.AsQueryable();
            query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada);

            if (!String.IsNullOrEmpty(text)) query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada &&
            (
                gc.Nome.ToUpper().Contains(text.ToUpper())
                || gc.Parentesco.ToUpper().Contains(text.ToUpper())
                || gc.Telefone.ToUpper().Contains(text.ToUpper())
                || (gc.Email ?? String.Empty).ToUpper().Contains(text.ToUpper())
            ));

            int totalCount = await query.CountAsync();

            if (take != null && offSet != null)
                return new AsyncOutResult<IEnumerable<Responsavel>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), totalCount);
            else
                return new AsyncOutResult<IEnumerable<Responsavel>, int>(await query.ToListAsync(), totalCount);

        }

    }

}
