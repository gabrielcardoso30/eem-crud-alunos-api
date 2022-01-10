using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Commands.Security.Handler
{
    public class UpdateGrupoCommandHandler : IRequestHandler<UpdateGrupoCommand, Result<GrupoResponse>>
    {
        private readonly IGrupoRepository _grupoRepository;
		private readonly IGrupoModuloRepository _grupoModuloRepository;
        private readonly IMapper _mapper;
		private readonly IGrupoUnidadeAcessoRepository _grupoUnidadeAcessoRepository;
		public UpdateGrupoCommandHandler(
			IGrupoRepository grupoRepository,
			IMapper mapper,
			IGrupoModuloRepository grupoModuloRepository,
			IGrupoUnidadeAcessoRepository grupoUnidadeAcessoRepository)
        {
			_grupoUnidadeAcessoRepository = grupoUnidadeAcessoRepository;
			_grupoModuloRepository = grupoModuloRepository;
            _grupoRepository = grupoRepository;
            _mapper = mapper;
        }
        public async Task<Result<GrupoResponse>> Handle(UpdateGrupoCommand request, CancellationToken cancellationToken)
        {
            var result = new Result<GrupoResponse>();

            var grupoUpdate = await _grupoRepository.GetById(request.Id);
            if (grupoUpdate == null)
            {
                result.WithNotFound("Grupo não encontrado!");
                return result;
            }

            if (String.IsNullOrEmpty(request.Nome))
            {
                result.WithError("Para editar um perfil é necessário informar o nome!");
                return result;
            }

            //// Validação se já existe um grupo com mesmo nome
            //var grupoComNomeJaExistente = await _grupoRepository.GetByName(request.Nome);
            //if (grupoComNomeJaExistente != null) {
            //    result.WithNotFound("Já existe um grupo para este nome e módulo");
            //    return result;
            //}

            grupoUpdate = _mapper.Map(request, grupoUpdate);

            if (await _grupoRepository.UpdateAsync(grupoUpdate))
            {

                IList<GrupoModulo> grupoModulosParaExclusao = await _grupoModuloRepository.GetByGrupoId(grupoUpdate.Id);
                if (grupoModulosParaExclusao.Count > 0) await _grupoModuloRepository.DeleteRangeAsync(grupoModulosParaExclusao);

                IList<GrupoModulo> grupoModulos = new List<GrupoModulo>();
                foreach (var item in request.Modulos) grupoModulos.Add(new GrupoModulo() { GrupoId = grupoUpdate.Id, Modulo = item });
                await _grupoModuloRepository.AddRangeAsync(grupoModulos);

                IList<GrupoUnidadeAcesso> grupoUnidadesAcessoParaExclusao = await _grupoUnidadeAcessoRepository.GetByGrupoId(grupoUpdate.Id);
                if (grupoUnidadesAcessoParaExclusao.Count > 0) await _grupoUnidadeAcessoRepository.DeleteRangeAsync(grupoUnidadesAcessoParaExclusao);

                IList<GrupoUnidadeAcesso> grupoUnidadesAcesso = new List<GrupoUnidadeAcesso>();
                foreach (var item in request.UnidadesAcesso) grupoUnidadesAcesso.Add(new GrupoUnidadeAcesso() { GrupoId = grupoUpdate.Id, UnidadeAcessoId = new Guid(item) });
                await _grupoUnidadeAcessoRepository.AddRangeAsync(grupoUnidadesAcesso);

            }
            
            result.Value = _mapper.Map<GrupoResponse>(grupoUpdate);

            return result;
        }
    }
}
