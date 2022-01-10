using AutoMapper;
using Core.Entities.Security;
using Core.Enums.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Commands.Security.Handler
{

    public class UpdateUnidadeAcessoCommandHandler : IRequestHandler<UpdateUnidadeAcessoCommand, Result<UnidadeAcessoResponse>>
    {

        private readonly IUnidadeAcessoRepository _repository;
		private readonly IUnidadeAcessoModuloRepository _unidadeAcessoModuloRepository;
        private readonly IMapper _mapper;

		public UpdateUnidadeAcessoCommandHandler(
			IUnidadeAcessoRepository repository,
			IMapper mapper,
			IUnidadeAcessoModuloRepository unidadeAcessoModuloRepository
        )
        {
			_unidadeAcessoModuloRepository = unidadeAcessoModuloRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UnidadeAcessoResponse>> Handle(UpdateUnidadeAcessoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<UnidadeAcessoResponse>();
            var oldRegister = await _repository.GetById(request.Id);

            if (oldRegister == null)
            {
                result.WithNotFound("Registro não encontrado!");
                return result;
            }

            if (String.IsNullOrEmpty(request.Request.Nome))
            {
                result.WithError("É necessário informar o nome!");
                return result;
            }
            
            if (request.Request.Modulos == null && request.Request.Modulos?.Count == 0)
            {
                result.WithError("É necessário informar pelo menos um módulo!");
                return result;
            }

            var registro = _mapper.Map(request.Request, oldRegister);
            var registroInserido = await _repository.UpdateAsync(oldRegister);
            if (registroInserido)
            {

                IList<UnidadeAcessoModulo> unidadeAcessoModulosParaExclusao = await _unidadeAcessoModuloRepository.GetByUnidadeAcessoId(registro.Id);
                if (unidadeAcessoModulosParaExclusao.Count > 0) await _unidadeAcessoModuloRepository.DeleteRangeAsync(unidadeAcessoModulosParaExclusao);

                IList<UnidadeAcessoModulo> unidadeAcessoModulos = new List<UnidadeAcessoModulo>();
                foreach (var item in request.Request.Modulos) unidadeAcessoModulos.Add(new UnidadeAcessoModulo() { UnidadeAcessoId = registro.Id, Modulo = item });
                await _unidadeAcessoModuloRepository.AddRangeAsync(unidadeAcessoModulos);

                IList<KeyValuePair<string, string>> modulosKeyValue = new List<KeyValuePair<string, string>>();
                IDictionary<string, string> dic = Enum.GetValues(typeof(EnumModulo)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
                foreach (var item in request.Request.Modulos) modulosKeyValue.Add(new KeyValuePair<string, string>(dic.Where(gc => gc.Value == item).FirstOrDefault().Key, item));

                UnidadeAcessoResponse unidadeAcessoResponse = _mapper.Map<UnidadeAcessoResponse>(registro);
                unidadeAcessoResponse.Modulos = modulosKeyValue.ToArray();
                result.Value = unidadeAcessoResponse;
                
            }

            return result;

        }

    }

}
