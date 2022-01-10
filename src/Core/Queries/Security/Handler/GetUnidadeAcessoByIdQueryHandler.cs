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

    public class GetUnidadeAcessoByIdQueryHandler : IRequestHandler<GetUnidadeAcessoByIdQuery, Result<UnidadeAcessoResponse>>
    {

        private readonly IUnidadeAcessoRepository _usuarioRepository;
		private readonly IUnidadeAcessoModuloRepository _unidadeAcessoModuloRepository;
        private readonly IMapper _mapper;

        public GetUnidadeAcessoByIdQueryHandler(
            IUnidadeAcessoRepository usuarioRepository,
            IMapper mapper,
			IUnidadeAcessoModuloRepository unidadeAcessoModuloRepository
        )
        {
			_unidadeAcessoModuloRepository = unidadeAcessoModuloRepository;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public async Task<Result<UnidadeAcessoResponse>> Handle(GetUnidadeAcessoByIdQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<UnidadeAcessoResponse>();
            var registro = await _usuarioRepository.GetById(query.Id);

            if (registro == null)
            {
                result.WithNotFound("Unidade de Acesso não encontrada!");
                return result;
            }

            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumModulo)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            IList<UnidadeAcessoModulo> unidadeAcessoModulos = await _unidadeAcessoModuloRepository.GetByUnidadeAcessoId(query.Id);
            IList<KeyValuePair<string, string>> modulosKeyValue = new List<KeyValuePair<string, string>>();
            foreach (var item in unidadeAcessoModulos) modulosKeyValue.Add(new KeyValuePair<string, string>(dic.Where(gc => gc.Value == item.Modulo).FirstOrDefault().Key, item.Modulo));

            UnidadeAcessoResponse response = _mapper.Map<UnidadeAcessoResponse>(registro);
            response.Modulos = modulosKeyValue.ToArray();
            result.Value = response;
            return result;

        }

    }

}
