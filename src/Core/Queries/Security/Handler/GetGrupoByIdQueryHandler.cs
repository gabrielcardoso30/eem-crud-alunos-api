using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Enums.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{
    public class GetGrupoByIdQueryHandler : IRequestHandler<GetGrupoByIdQuery, Result<GrupoResponse>>
    {
        private readonly IGrupoRepository _grupoRepository;
		private readonly IGrupoModuloRepository _grupoModuloRepository;
        private readonly IMapper _mapper;
		private readonly IGrupoUnidadeAcessoRepository _grupoUnidadeAcessoRepository;
		private readonly IUnidadeAcessoRepository _unidadeAcessoRepository;
		public GetGrupoByIdQueryHandler(
			IGrupoRepository grupoRepository,
			IMapper mapper,
			IGrupoModuloRepository grupoModuloRepository,
			IUnidadeAcessoRepository unidadeAcessoRepository,
			IGrupoUnidadeAcessoRepository grupoUnidadeAcessoRepository
        )
        {
			_grupoUnidadeAcessoRepository = grupoUnidadeAcessoRepository;
			_unidadeAcessoRepository = unidadeAcessoRepository;
			_grupoModuloRepository = grupoModuloRepository;
            _grupoRepository = grupoRepository;
            _mapper = mapper;
        }
        public async Task<Result<GrupoResponse>> Handle(GetGrupoByIdQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<GrupoResponse>();

            var grupo = await _grupoRepository.GetById(query.Id);
            if (grupo == null)
            {
                result.WithNotFound("Grupo não encontrado!");
                return result;
            }

            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumModulo)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            IList<GrupoModulo> grupoModulos = await _grupoModuloRepository.GetByGrupoId(query.Id);
            IList<KeyValuePair<string, string>> modulosKeyValue = new List<KeyValuePair<string, string>>();
            foreach (var item in grupoModulos) modulosKeyValue.Add(new KeyValuePair<string, string>(dic.Where(gc => gc.Value == item.Modulo).FirstOrDefault().Key, item.Modulo));

            IList<GrupoUnidadeAcesso> grupoUnidadesAcesso = await _grupoUnidadeAcessoRepository.GetByGrupoId(query.Id);
            IList<UnidadeAcesso> unidadesAcesso = await _unidadeAcessoRepository.Get(grupoUnidadesAcesso.Select(gc => gc.UnidadeAcessoId).ToArray());

            GrupoResponse response = _mapper.Map<GrupoResponse>(grupo);
            response.Modulos = modulosKeyValue.ToArray();
            response.UnidadesAcesso = unidadesAcesso.Select(p => _mapper.Map<UnidadeAcessoResponse>(p));
            result.Value = response;

            return result;
        }
    }
}
