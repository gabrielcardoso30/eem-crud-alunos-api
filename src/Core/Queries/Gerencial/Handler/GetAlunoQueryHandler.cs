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

    public class GetAlunoQueryHandler : IRequestHandler<GetAlunoQuery, Result<IEnumerable<AlunoResponse>>>
    {

        private readonly IAlunoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
		private readonly IResponsavelRepository _responsavelRepository;
		private readonly IUsuarioRepository _usuarioRepository;

        public GetAlunoQueryHandler(
            IAlunoRepository repository, 
            IMapper mapper,
            IAuthenticatedUser authenticatedUser,
			IUsuarioRepository usuarioRepository,
			IResponsavelRepository responsavelRepository
        )
        {
			_responsavelRepository = responsavelRepository;
			_usuarioRepository = usuarioRepository;
            _repository = repository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
        }

        public async Task<Result<IEnumerable<AlunoResponse>>> Handle(GetAlunoQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<AlunoResponse>>();
            Guid unidadeAcessoId = await _repository.GetSelectedAccessUnitIdAsync();

            var registros = await _repository.SearchAll(query.Filter.Text, query.Filter.Take, query.Filter.Skip, query.Filter.TableFilter);
                        
            IEnumerable<AlunoResponse> enumerable = registros.Result(out var count).Select(p => _mapper.Map<AlunoResponse>(p)).ToList();
            IList<Responsavel> responsaveis = await _responsavelRepository.GetByAlunosId(enumerable.Select(gc => Guid.Parse(gc.Id)).ToArray());
            foreach (var item in enumerable) item.Responsaveis = responsaveis.Where(gc => Convert.ToString(gc.AlunoId).ToUpper() == item.Id).Select(p => _mapper.Map<ResponsavelResponse>(p)).ToArray();
            
            if (!String.IsNullOrEmpty(query.Filter.SortingProp))
            {
                string nomeColuna = query.Filter.SortingProp.ToUpper();
                bool ascending = query.Filter.Ascending;
                if (nomeColuna is "NOME") enumerable = (ascending ? enumerable.OrderBy(gc => gc.Nome) : enumerable.OrderByDescending(gc => gc.Nome)).ToList();
                else if (nomeColuna is "DATANASCIMENTOFORMATADA" || nomeColuna is "DATANASCIMENTO") enumerable = (ascending ? enumerable.OrderBy(gc => gc.DataNascimento) : enumerable.OrderByDescending(gc => gc.DataNascimento)).ToList();
                else if (nomeColuna is "SEGMENTODESCRICAO" || nomeColuna is "SEGMENTO") enumerable = (ascending ? enumerable.OrderBy(gc => gc.Segmento) : enumerable.OrderByDescending(gc => gc.Segmento)).ToList();
                else if (nomeColuna is "EMAIL") enumerable = (ascending ? enumerable.OrderBy(gc => gc.Email) : enumerable.OrderByDescending(gc => gc.Email)).ToList();
            }
            else enumerable = enumerable.OrderBy(x => x.Nome).ToList();

            result.Value = enumerable.ToArray();
            result.Count = count;
            result.TableCount = await _repository.CountTable();

            return result;

        }

    }

}
