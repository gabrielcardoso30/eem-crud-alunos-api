using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using Core.Models.Request.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Enums.Security;

namespace Core.Commands.Security.Handler
{

    public class CreateUnidadeAcessoCommandHandler : IRequestHandler<CreateUnidadeAcessoCommand, Result<UnidadeAcessoResponse>>
    {

        private readonly IUnidadeAcessoRepository _repository;
		private readonly IUnidadeAcessoModuloRepository _unidadeAcessoModuloRepository;
        private readonly IMapper _mapper;

		public CreateUnidadeAcessoCommandHandler(
			IUnidadeAcessoRepository repository,
			IMapper mapper,
			IUnidadeAcessoModuloRepository unidadeAcessoModuloRepository
        )
        {
			_unidadeAcessoModuloRepository = unidadeAcessoModuloRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UnidadeAcessoResponse>> Handle(CreateUnidadeAcessoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<UnidadeAcessoResponse>();

            if (String.IsNullOrEmpty(request.Request.Nome))
            {
                result.WithError("É necessário informar o nome!");
                return result;
            }

            if(request.Request.Modulos == null && request.Request.Modulos?.Count == 0)
            {
                result.WithError("É necessário informar pelo menos um módulo!");
                return result;
            }

            var registro = _mapper.Map<UnidadeAcesso>(request.Request);
            var registroInserido = await _repository.AddAsync(registro);

            IList<UnidadeAcessoModulo> unidadeAcessoModulos = new List<UnidadeAcessoModulo>();
            foreach (var item in request.Request.Modulos) unidadeAcessoModulos.Add(new UnidadeAcessoModulo(){ UnidadeAcessoId = registro.Id, Modulo = item });
            await _unidadeAcessoModuloRepository.AddRangeAsync(unidadeAcessoModulos);

            IList<KeyValuePair<string, string>> modulosKeyValue = new List<KeyValuePair<string, string>>();
            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumModulo)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            foreach (var item in request.Request.Modulos) modulosKeyValue.Add(new KeyValuePair<string, string>(dic.Where(gc => gc.Value == item).FirstOrDefault().Key, item));

            UnidadeAcessoResponse unidadeAcessoResponse = _mapper.Map<UnidadeAcessoResponse>(registroInserido);
            unidadeAcessoResponse.Modulos = modulosKeyValue.ToArray();
            result.Value = unidadeAcessoResponse;

            return result;

        }

    }

}
