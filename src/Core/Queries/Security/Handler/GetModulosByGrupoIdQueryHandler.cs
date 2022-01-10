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
    
    public class GetModulosByGrupoIdQueryHandler : IRequestHandler<GetModulosByGrupoIdQuery, Result<IEnumerable<KeyValuePair<string, string>>>>
    {

        private readonly IGrupoModuloRepository _grupoModuloRepository;
        private readonly IMapper _mapper;

        public GetModulosByGrupoIdQueryHandler(
            IGrupoModuloRepository grupoModuloRepository, 
            IMapper mapper
        )
        {
            _grupoModuloRepository = grupoModuloRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<KeyValuePair<string, string>>>> Handle(GetModulosByGrupoIdQuery query, CancellationToken cancellationToken)
        {

            var result = new Result<IEnumerable<KeyValuePair<string, string>>>();

            if (query.Id == null || query.Id == Guid.Empty)
            {
                result.WithNotFound("Registro não encontrado.");
                return result;
            }

            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumModulo)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            IList<GrupoModulo> grupoModulos = await _grupoModuloRepository.GetByGrupoId(query.Id);
            IList<KeyValuePair<string, string>> modulosKeyValue = new List<KeyValuePair<string, string>>();
            foreach (var item in grupoModulos) modulosKeyValue.Add(new KeyValuePair<string, string>(dic.Where(gc => gc.Value == item.Modulo).FirstOrDefault().Key, item.Modulo));

            result.Value = modulosKeyValue.ToArray();
            return result;

        }

    }

}
