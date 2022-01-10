using AutoMapper;
using Core.Entities.Gerencial;
using Core.Helpers;
using Core.Interfaces.Repositories.Gerencial;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
using Core.Models.Responses.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Queries.Gerencial.Handler
{

    public class GetResponsavelQueryHandler : IRequestHandler<GetResponsavelQuery, Result<IEnumerable<ResponsavelResponse>>>
    {

        private readonly IResponsavelRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
		private readonly IResponsavelRepository _responsavelRepository;
		private readonly IUsuarioRepository _usuarioRepository;
		private readonly IAlunoRepository _alunoRepository;

        public GetResponsavelQueryHandler(
            IResponsavelRepository repository, 
            IMapper mapper,
            IAuthenticatedUser authenticatedUser,
			IUsuarioRepository usuarioRepository,
			IResponsavelRepository responsavelRepository,
			IAlunoRepository alunoRepository
        )
        {
			_alunoRepository = alunoRepository;
			_responsavelRepository = responsavelRepository;
			_usuarioRepository = usuarioRepository;
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<IEnumerable<ResponsavelResponse>>> Handle(GetResponsavelQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<ResponsavelResponse>>();
            Guid unidadeAcessoId = await _repository.GetSelectedAccessUnitIdAsync();

            var registros = await _repository.SearchAll(query.Filter.Text, query.Filter.Take, query.Filter.Skip, query.Filter.TableFilter);
                        
            IEnumerable<ResponsavelResponse> enumerable = registros.Result(out var count).Select(p => _mapper.Map<ResponsavelResponse>(p)).ToList();
            IList<Aluno> alunos = await _alunoRepository.Get(enumerable.Select(gc => Guid.Parse(gc.AlunoId)).ToArray());
            foreach (var item in enumerable) item.AlunoNome = alunos.Where(gc => Convert.ToString(gc.Id).ToUpper() == item.AlunoId).FirstOrDefault()?.Nome ?? String.Empty;
            
            if (!String.IsNullOrEmpty(query.Filter.SortingProp))
            {
                string nomeColuna = query.Filter.SortingProp.ToUpper();
                bool ascending = query.Filter.Ascending;
                if (nomeColuna is "NOME") enumerable = (ascending ? enumerable.OrderBy(gc => gc.Nome) : enumerable.OrderByDescending(gc => gc.Nome)).ToList();
                else if (nomeColuna is "DATANASCIMENTOFORMATADA" || nomeColuna is "DATANASCIMENTO") enumerable = (ascending ? enumerable.OrderBy(gc => gc.DataNascimento) : enumerable.OrderByDescending(gc => gc.DataNascimento)).ToList();
                else if (nomeColuna is "PARENTESCODESCRICAO" || nomeColuna is "PARENTESCO") enumerable = (ascending ? enumerable.OrderBy(gc => gc.Parentesco) : enumerable.OrderByDescending(gc => gc.Parentesco)).ToList();
                else if (nomeColuna is "EMAIL") enumerable = (ascending ? enumerable.OrderBy(gc => gc.Email) : enumerable.OrderByDescending(gc => gc.Email)).ToList();
                else if (nomeColuna is "TELEFONE") enumerable = (ascending ? enumerable.OrderBy(gc => gc.Telefone) : enumerable.OrderByDescending(gc => gc.Telefone)).ToList();
                else if (nomeColuna is "ALUNONOME") enumerable = (ascending ? enumerable.OrderBy(gc => gc.AlunoNome) : enumerable.OrderByDescending(gc => gc.AlunoNome)).ToList();
            }
            else enumerable = enumerable.OrderBy(x => x.Nome).ToList();

            result.Value = enumerable.ToArray();
            result.Count = count;
            result.TableCount = await _repository.CountTable();

            return result;

        }

    }

}
