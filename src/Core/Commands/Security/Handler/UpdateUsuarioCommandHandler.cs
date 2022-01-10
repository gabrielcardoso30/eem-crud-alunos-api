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
using Core.Interfaces.Services;
using Core.Models.Responses.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Core.Commands.Security.Handler
{

    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, Result<UsuarioResponse>>
    {

        private UserManager<ApplicationUser> _userManager;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IBlobStorage _blobStorage;
        private readonly AppSettings _appSettings;
        private readonly EnvironmentVariables _environmentVariables;
        private readonly IMsSendMail _msSendMail;

        public UpdateUsuarioCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUsuarioRepository usuarioRepository,
            IMapper mapper,
            IAuthenticatedUser authenticatedUser,
            IBlobStorage blobStorage,
            IOptions<AppSettings> appSettings,
            EnvironmentVariables environmentVariables,
            IMsSendMail msSendMail
        )
        {
            _userManager = userManager;
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _authenticatedUser = authenticatedUser;
            _blobStorage = blobStorage;
            _appSettings = appSettings.Value;
            _environmentVariables = environmentVariables;
            _msSendMail = msSendMail;
        }

        public async Task<Result<UsuarioResponse>> Handle(
            UpdateUsuarioCommand request,
            CancellationToken cancellationToken
        )
        {

            var result = new Result<UsuarioResponse>();

            if (String.IsNullOrEmpty(request.Nome))
            {
                result.WithError("O campo Nome é de carater obrigatório.");
                return result;
            }
            if (String.IsNullOrEmpty(request.Login))
            {
                result.WithError("O campo Login é de carater obrigatório.");
                return result;
            }

            var applicationUser = await _userManager.FindByNameAsync(request.Login.ToString());
            if (applicationUser == null)
            {
                result.WithNotFound("Usuário não encontrado!");
                return result;
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                var emailJaExiste = await _usuarioRepository.ExistsEmail(request.Login.ToString(), request.Email);
                if (emailJaExiste)
                {
                    result.WithError("Email já existe!");
                    return result;
                }
            }

            applicationUser.PhoneNumber = request.Telefone;
            applicationUser.TelefoneResidencial = request.TelefoneResidencial;

            //if (request.File != null)
            //{
            //    var blobReference = $@"{Guid.NewGuid().ToString()}{Path.GetExtension(request.File.FileName)}";
            //    var imgUrl = await _blobStorage.UploadFileAsync("Fotos", request.File, blobReference);
            //    applicationUser.UrlImagem = imgUrl;
            //}

            bool usuarioAtivo = applicationUser.Ativo;
            var usuarioUpdate = _mapper.Map(request, applicationUser);

            usuarioUpdate.TelefoneResidencial = request.TelefoneResidencial;
            usuarioUpdate.PhoneNumber = request.Telefone;
            usuarioUpdate.Email = request.Email;
            usuarioUpdate.Ativo = usuarioAtivo;

            var x = await _userManager.UpdateAsync(usuarioUpdate);
            var usuario = _mapper.Map<AspNetUsers>(applicationUser);
            result.Value = _mapper.Map<UsuarioResponse>(usuario);

            if (!String.IsNullOrEmpty(request.Senha) && !String.IsNullOrEmpty(request.SenhaConfirmacao))
            {

                if (request.Senha == request.SenhaConfirmacao)
                {

                    string resetToken = await _userManager.GeneratePasswordResetTokenAsync(usuarioUpdate);
                    IdentityResult passwordChangeResult = await _userManager.ResetPasswordAsync(usuarioUpdate, resetToken, request.Senha);

                    if(!passwordChangeResult.Succeeded)
                    {
                        string erros = String.Empty;
                        foreach (var item in passwordChangeResult.Errors)
                        {
                            erros += String.Concat(" ", item.Description);
                        }
                        result.WithError(erros);
                        return result;
                    }  

                    result.Value.Senha = request.Senha;

                }
                else
                {

                    result.WithError("Senha e Confirmação de Senha não conferem!");
                    return result;

                }

            }
            else if ((String.IsNullOrEmpty(request.Senha) || String.IsNullOrEmpty(request.SenhaConfirmacao)))
            {

                result.Value.Senha = "A Senha permanece a mesma.";

            }

            return result;

        }

    }

}
