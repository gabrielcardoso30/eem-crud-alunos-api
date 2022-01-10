using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Services;
using Core.Models.Responses.Security;
using Core.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Core.Commands.Security.Handler
{

    public class CreateUsuarioCommandHandler : IRequestHandler<CreateUsuarioCommand, Result<UsuarioResponse>>
    {

        private ApplicationUser _applicationUser;
        private UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IBlobStorage _blobStorage;
        private readonly IMsSendMail _msSendMail;

        public CreateUsuarioCommandHandler(
            UserManager<ApplicationUser> userManager,
            IUsuarioRepository usuarioRepositoy,
            IMapper mapper,
            IBlobStorage blobStorage,
            IMsSendMail msSendMail)
        {
            _userManager = userManager;
            _mapper = mapper;
            _usuarioRepository = usuarioRepositoy;
            _blobStorage = blobStorage;
            _msSendMail = msSendMail;
        }

        public async Task<Result<UsuarioResponse>> Handle(
            CreateUsuarioCommand request,
            CancellationToken cancellationToken
        )
        {

            var resultado = new IdentityResult();
            var result = new Result<UsuarioResponse>();

            if (String.IsNullOrEmpty(request.Nome))
            {
                result.WithError("O campo Nome é de carater obrigatório.");
                return result;
            }
            if (String.IsNullOrEmpty(request.Login) || request.Login.Length < 5)
            {
                result.WithError("O campo Login é de carater obrigatório e deve possuir ao menos 5 caracteres.");
                return result;
            }

            _applicationUser = await _userManager.FindByNameAsync(request.Login);
            if (_applicationUser != null)
            {
                result.WithError("Já existe um usuário com esse login!");
                return result;
            }

            var emailJaExiste = await _usuarioRepository.ExistsEmail(request.Email);
            if (emailJaExiste)
            {
                result.WithError("Email já existe!");
                return result;
            }

            _applicationUser = new ApplicationUser
            {
                UserName = request.Login.ToLower(),
                Email = request.Email.ToLower(),
                PhoneNumber = request.Telefone,
                NormalizedUserName = request.Login,
                Nome = request.Nome,
                DataCriacao = DateTime.Now,
                Deletado = false,
                TipoUsuario = request.TipoUsuario,
                CPF = request.Cpf,
                Ativo = true
            };

            //if (request.File != null)
            //{
            //    var blobReference = $@"{Guid.NewGuid().ToString()}{Path.GetExtension(request.File.FileName)}";
            //    var imgUrl = await _blobStorage.UploadFileAsync("Fotos", request.File, blobReference);
            //    _applicationUser.CaminhoFoto = imgUrl;
            //}

            // var password = AccessManager.GeneratePassword(10, 10);
            var password = "invix@123";
            resultado = await _userManager.CreateAsync(_applicationUser, password);

            if (!resultado.Succeeded) return null;

            var response = _mapper.Map<AspNetUsers>(_applicationUser);
            result.Value = _mapper.Map<UsuarioResponse>(response);
            result.Value.Senha = password;

            // if (_applicationUser.Email != null)
            // {

            //     var message = @"Dados de acesso para a conta de " + request.Nome.ToUpper() + " <br>" + "Login: " + request.Login + "<br>" + "Matrícula: " + (request.Matricula ?? "") + "<br>" + "Senha: " + password;
            //     await _msSendMail.SendMailAsync(_applicationUser.Email, "Dados de Acesso", message, "Usuário", _applicationUser.Id.ToString(), 0);

            // }

            return result;

        }

    }

}