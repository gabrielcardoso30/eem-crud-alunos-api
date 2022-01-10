using Core.Entities.Gerencial;
using Core.Helpers;
using Core.Interfaces.Repositories.Gerencial;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infra.Data.Repositories.Rdo
{

    public class AlunoRepository : BaseRepository<Aluno>, IAlunoRepository
    {

        private readonly AppDbContext _dbContext;
        private readonly Guid _unidadeAcessoSelecionada;

        public AlunoRepository(
            AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _unidadeAcessoSelecionada = GetSelectedAccessUnitId();
        }

        public async Task<IList<Aluno>> Get(IEnumerable<Guid> ids)
        {

            var query = _dbContext.Aluno.AsQueryable();

            if (ids != null) query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada && ids.Contains(gc.Id));

            return await query.ToListAsync();

        }

        public async Task<IList<Aluno>> Search(string text, int? take, int? offSet)
        {

            var query = _dbContext.Aluno.AsQueryable();
            if (!String.IsNullOrEmpty(text)) query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada &&
            (
                gc.Nome.Contains(text)
                || gc.Segmento.ToUpper().Contains(text.ToUpper())
                || gc.Email.ToUpper().Contains(text.ToUpper())
            ));

            if (take != null && offSet != null) return await query.Skip((int)offSet).Take((int)take).ToListAsync();
            else return await query.ToListAsync();

        }

        public async Task<AsyncOutResult<IEnumerable<Aluno>, int>> SearchAll(string text, int? take, int? offSet, string tableFilter)
        {

            var query = _dbContext.Aluno.AsQueryable();
            bool realizouFiltro = true;
            query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada);
            if (!String.IsNullOrEmpty(text))
            {
                query = query.Where(gc => gc.UnidadeAcessoId == _unidadeAcessoSelecionada &&
                (
                    gc.Nome.Contains(text)
                    || gc.Segmento.ToUpper().Contains(text.ToUpper())
                    || gc.Email.ToUpper().Contains(text.ToUpper())
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
                    string segmento = tableFilterList.Where(gc => gc.Key.ToUpper() == "SEGMENTO").FirstOrDefault().Value;
                    string responsavelNome = tableFilterList.Where(gc => gc.Key.ToUpper() == "RESPONSAVEL").FirstOrDefault().Value;

                    if (!String.IsNullOrEmpty(nome))
                    {
                        realizouFiltro = true;
                        query = query.Where(gc => gc.Nome.ToUpper().Contains(nome.ToUpper()));
                    }
                    if (!String.IsNullOrEmpty(segmento))
                    {
                        realizouFiltro = true;
                        query = query.Where(gc => gc.Segmento.ToUpper().Contains(segmento.ToUpper()));
                    }
                    if (!String.IsNullOrEmpty(responsavelNome))
                    {
                        Responsavel responsavel = await _dbContext.Responsavel.Where(gc => gc.Nome.ToUpper().Contains(responsavelNome.ToUpper())).FirstOrDefaultAsync();
                        if (responsavel != null)
                        {
                            realizouFiltro = true;
                            query = query.Where(gc => gc.Id == responsavel.AlunoId);
                        }
                    }

                }

            }

            if (!realizouFiltro) query = query.Where(gc => gc.Id == Guid.Empty);
            int totalCount = await query.CountAsync();

            if (take != null && offSet != null)
                return new AsyncOutResult<IEnumerable<Aluno>, int>(await query.Skip((int)offSet).Take((int)take).ToListAsync(), totalCount);
            else
                return new AsyncOutResult<IEnumerable<Aluno>, int>(await query.ToListAsync(), totalCount);

        }

    }

}
