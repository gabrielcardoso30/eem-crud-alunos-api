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

    public class CreateGrupoCommandHandler : IRequestHandler<CreateGrupoCommand, Result<GrupoResponse>>
    {

        private readonly IGrupoRepository _grupoRepository;
		private readonly IGrupoModuloRepository _grupoModuloRepository;
        private readonly IMapper _mapper;
		private readonly IGrupoUnidadeAcessoRepository _grupoUnidadeAcessoRepository;

        public CreateGrupoCommandHandler(
            IGrupoRepository grupoRepository, 
            IMapper mapper,
			IGrupoModuloRepository grupoModuloRepository,
			IGrupoUnidadeAcessoRepository grupoUnidadeAcessoRepository
        )
        {
			_grupoUnidadeAcessoRepository = grupoUnidadeAcessoRepository;
			_grupoModuloRepository = grupoModuloRepository;
            _grupoRepository = grupoRepository;
            _mapper = mapper;
        }

        public async Task<Result<GrupoResponse>> Handle(CreateGrupoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<GrupoResponse>();

            if (String.IsNullOrEmpty(request.Nome))
            {
                result.WithError("Para criar um perfil é necessário informar o nome!");
                return result;
            }

            var grupoExiste = await _grupoRepository.GetByName(request.Nome);
            if (grupoExiste != null)
            {
                result.WithError("Já existe um grupo com esse nome!");
                return result;
            }

            var grupoNew = _mapper.Map<Grupo>(request);
            var grupo = await _grupoRepository.AddAsync(grupoNew);

            IList<GrupoModulo> grupoModulos = new List<GrupoModulo>();
            foreach (var item in request.Modulos) grupoModulos.Add(new GrupoModulo() { GrupoId = grupo.Id, Modulo = item });
            await _grupoModuloRepository.AddRangeAsync(grupoModulos);

            IList<GrupoUnidadeAcesso> grupoUnidadesAcesso = new List<GrupoUnidadeAcesso>();
            foreach (var item in request.UnidadesAcesso) grupoUnidadesAcesso.Add(new GrupoUnidadeAcesso() { GrupoId = grupo.Id, UnidadeAcessoId = new Guid(item) });
            await _grupoUnidadeAcessoRepository.AddRangeAsync(grupoUnidadesAcesso);

            result.Value = _mapper.Map<GrupoResponse>(grupo);

            return result;

        }

    }

}