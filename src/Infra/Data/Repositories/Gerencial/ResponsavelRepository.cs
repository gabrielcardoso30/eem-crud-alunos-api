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
            bool realizouFiltro = true;
            query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada);

            if (!String.IsNullOrEmpty(text)) 
            {
                query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada &&
                (
                    gc.Nome.ToUpper().Contains(text.ToUpper())
                    || gc.Parentesco.ToUpper().Contains(text.ToUpper())
                    || gc.Telefone.ToUpper().Contains(text.ToUpper())
                    || (gc.Email ?? String.Empty).ToUpper().Contains(text.ToUpper())
                ));
            }

            IList<KeyValuePair<string, string>> tableFilterList = new List<KeyValuePair<string, string>>();
            int nullOrEmptyFields = 0;
            if (!String.IsNullOrEmpty(tableFilter)) tableFilterList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KeyValuePair<string, string>>>(tableFilter);

            if (tableFilterList.Count > 0)
            {

                foreach (var item in tableFilterList) if (String.IsNullOrEmpty(item.Value)) nullOrEmptyFields++;
                if (nullOrEmptyFields != tableFilterList.Count)
                {

                    realizouFiltro = false;
                    string nome = tableFilterList.Where(gc => gc.Key.ToUpper() == "NOME").FirstOrDefault().Value;
                    string parentesco = tableFilterList.Where(gc => gc.Key.ToUpper() == "PARENTESCO").FirstOrDefault().Value;
                    string alunoNome = tableFilterList.Where(gc => gc.Key.ToUpper() == "ALUNO").FirstOrDefault().Value;
                    string telefone = tableFilterList.Where(gc => gc.Key.ToUpper() == "TELEFONE").FirstOrDefault().Value;

                    if (!String.IsNullOrEmpty(nome))
                    {
                        realizouFiltro = true;
                        query = query.Where(gc => gc.Nome.ToUpper().Contains(nome.ToUpper()));
                    }
                    if (!String.IsNullOrEmpty(parentesco))
                    {
                        realizouFiltro = true;
                        query = query.Where(gc => gc.Parentesco.ToUpper().Contains(parentesco.ToUpper()));
                    }
                    if (!String.IsNullOrEmpty(telefone))
                    {
                        realizouFiltro = true;
                        query = query.Where(gc => gc.Telefone.ToUpper().Contains(telefone.ToUpper()));
                    }
                    if (!String.IsNullOrEmpty(alunoNome))
                    {
                        Aluno aluno = await _dbContext.Aluno.Where(gc => gc.Nome.ToUpper().Contains(alunoNome.ToUpper())).FirstOrDefaultAsync();
                        if (aluno != null)
                        {
                            realizouFiltro = true;
                            query = query.Where(gc => gc.AlunoId == aluno.Id);
                        }
                    }

                }

            }

            if (!realizouFiltro) query = query.Where(gc => gc.Id == Guid.Empty);
            int totalCount = await query.CountAsync();

            if (take != null && offSet != null)
                return new AsyncOutResult<IEnumerable<Responsavel>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), totalCount);
            else
                return new AsyncOutResult<IEnumerable<Responsavel>, int>(await query.ToListAsync(), totalCount);

        }

    }

}
