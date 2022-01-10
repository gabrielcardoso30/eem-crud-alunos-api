using AutoMapper;
using Core.Entities.Security;
using Core.Enums.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
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

    public class UpdateUsuarioUnidadeAcessoCommandHandler : IRequestHandler<UpdateUsuarioUnidadeAcessoCommand, Result<UsuarioResponse>>
    {

        private readonly IUsuarioRepository _repository;
        private readonly IUnidadeAcessoModuloRepository _unidadeAcessoModuloRepository;
        private readonly IMapper _mapper;
		private readonly IUnidadeAcessoRepository _unidadeAcessoRepository;
		private readonly IAuthenticatedUser _authenticatedUser;

        public UpdateUsuarioUnidadeAcessoCommandHandler(
            IUsuarioRepository repository,
            IMapper mapper,
            IUnidadeAcessoModuloRepository unidadeAcessoModuloRepository,
			IAuthenticatedUser authenticatedUser,
			IUnidadeAcessoRepository unidadeAcessoRepository
        )
        {
			_unidadeAcessoRepository = unidadeAcessoRepository;
			_authenticatedUser = authenticatedUser;
            _unidadeAcessoModuloRepository = unidadeAcessoModuloRepository;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UsuarioResponse>> Handle(UpdateUsuarioUnidadeAcessoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<UsuarioResponse>();
            Guid usuarioAutenticadoId = _authenticatedUser.GuidLogin();
            Guid usuarioId = (request.Id == null || request.Id == Guid.Empty ? usuarioAutenticadoId : request.Id);
            var oldRegister = await _repository.GetById(usuarioId);

            if (oldRegister == null)
            {
                result.WithNotFound("Registro não encontrado!");
                return result;
            }

            if (request.Request.UnidadeAcessoSelecionada == null || request.Request.UnidadeAcessoSelecionada == Guid.Empty)
            {
                result.WithError("É necessário informar uma unidade de acesso que seja válida!");
                return result;
            }

            UnidadeAcesso unidadeAcesso = await _unidadeAcessoRepository.GetById(request.Request.UnidadeAcessoSelecionada);
            if (unidadeAcesso == null)
            {
                result.WithError("É necessário informar uma unidade de acesso que seja válida!");
                return result;
            }

            oldRegister.UnidadeAcessoSelecionada = request.Request.UnidadeAcessoSelecionada;
            await _repository.UpdateAsync(oldRegister);
            result.Value = new UsuarioResponse();
            result.Value.SelectedAccessUnitId = unidadeAcesso.Id.ToString().ToUpper();
            result.Value.SelectedAccessUnitName = unidadeAcesso.Nome;

            return result;

        }

    }

}
