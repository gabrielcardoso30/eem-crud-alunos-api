using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Core.Commands.Security.Handler
{

    public class UpdateUsuarioStatusCommandHandler : IRequestHandler<UpdateUsuarioStatusCommand, Result<UsuarioResponse>>
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IBlobStorage _blobStorage;
        private readonly AppSettings _appSettings;
        private readonly EnvironmentVariables _environmentVariables;

        public UpdateUsuarioStatusCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            IAuthenticatedUser authenticatedUser,
            IBlobStorage blobStorage,
            IOptions<AppSettings> appSettings,
            EnvironmentVariables environmentVariables
        )
        {
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _blobStorage = blobStorage;
            _appSettings = appSettings.Value;
            _environmentVariables = environmentVariables;
        }

        public async Task<Result<UsuarioResponse>> Handle(
            UpdateUsuarioStatusCommand request,
            CancellationToken cancellationToken
        )
        {

            var result = new Result<UsuarioResponse>();

            if (request.Id == null || request.Id == Guid.Empty)
            {
                result.WithError("Parâmetros inválidos");
                return result;
            }

            var applicationUser = await _usuarioRepository.GetById(request.Id);
            if (applicationUser == null)
            {
                result.WithNotFound("Usuário não encontrado!");
                return result;
            }

            var usuarioUpdate = _mapper.Map(request, applicationUser);

            //usuarioUpdate.Ativo = request.Ativo;
            usuarioUpdate.Ativo = !usuarioUpdate.Ativo;
            usuarioUpdate.QuantidadeLogin = 0;
            usuarioUpdate.QuantidadePrimeiroAcesso = 0;
            usuarioUpdate.DataBloqueioPrimeiroAcesso = null;

            if (applicationUser.UserName == "admin" && !usuarioUpdate.Ativo)
            {
                result.WithError("O usuário administrador do sistema não pode ser inativado!");
                return result;
            }

            var x = await _userManager.UpdateAsync(usuarioUpdate);
            var perfil = _mapper.Map<AspNetUsers>(applicationUser);
            result.Value = _mapper.Map<UsuarioResponse>(perfil);

            return result;

        }

    }

}
