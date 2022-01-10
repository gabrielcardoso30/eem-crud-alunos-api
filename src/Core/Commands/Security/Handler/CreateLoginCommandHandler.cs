using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Entities.Security;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Interfaces.Security;
using Core.Models.Responses.Security;
using Core.Security;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Core.Commands.Security.Handler
{

    public class CreateLoginCommandHandler : IRequestHandler<CreateLoginCommand, Result<LoginResponse>>
    {

        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPermissaoUsuarioRepository _permissaoUsuarioRepository;
        private UserManager<ApplicationUser> _userManager;
        private AccessManager _accessManager;
        private AppSettings _appSettings;
        private EnvironmentVariables _environmentVariables;
        private bool _tokenTimeApp = false;
        private readonly ITokenManager _tokenManager;
        private readonly IUnidadeAcessoRepository _unidadeAcessoRepository;
        private readonly IGrupoModuloRepository _grupoModuloRepository;
        private readonly IPermissaoRepository _permissaoRepository;
        private readonly IPermissaoGrupoRepository _permissaoGrupoRepository;
        private readonly IGrupoUnidadeAcessoRepository _grupoUnidadeAcessoRepository;

        public CreateLoginCommandHandler(IUsuarioRepository usuarioRepository,
            AccessManager accessManager,
            IPermissaoUsuarioRepository permissaoUsuarioRepository,
            UserManager<ApplicationUser> userManager,
            IOptions<AppSettings> appSettings,
            EnvironmentVariables environmentVariables,
            ITokenManager tokenManager,
            IGrupoModuloRepository grupoModuloRepository,
            IUnidadeAcessoRepository unidadeAcessoRepository,
            IPermissaoGrupoRepository permissaoGrupoRepository,
            IPermissaoRepository permissaoRepository,
            IGrupoUnidadeAcessoRepository grupoUnidadeAcessoRepository
        )
        {
            _grupoUnidadeAcessoRepository = grupoUnidadeAcessoRepository;
            _permissaoRepository = permissaoRepository;
            _permissaoGrupoRepository = permissaoGrupoRepository;
            _unidadeAcessoRepository = unidadeAcessoRepository;
            _grupoModuloRepository = grupoModuloRepository;
            _usuarioRepository = usuarioRepository;
            _accessManager = accessManager;
            _permissaoUsuarioRepository = permissaoUsuarioRepository;
            _appSettings = appSettings.Value;
            _environmentVariables = environmentVariables;
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        public async Task<Result<LoginResponse>> Handle(CreateLoginCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<LoginResponse>();

            // Inicializa com falha.
            var loginResponse = new LoginResponse();

            switch (request.GrantTypes)
            {
                case "password":

                    ApplicationUser userIdentity = null;

                    if (String.IsNullOrEmpty(request.UserName) || request?.UserName?.Length < 4)
                    {

                        loginResponse.Message = "Login incorreto.";
                        result.WithError("Login incorreto.");
                        result.Value = loginResponse;
                        return result;

                    }
                    else
                    {

                        userIdentity = await _userManager.FindByNameAsync(request.UserName);

                    }

                    //USUÁRIO ADMIN NÃO PODE FICAR SEM ACESSAR O SISTEMA
                    if (userIdentity != null && userIdentity?.Id.ToString().ToUpper() == "97E7E460-FC41-4924-AC91-C1AFE5813559")
                    {

                        //CONFIGURA O USUÁRIO PARA ATIVO E NÃO DELETADO
                        AspNetUsers user = await _usuarioRepository.GetById(userIdentity.Id);
                        user.Deletado = false;
                        user.Ativo = true;

                        //CASO O USUÁRIO ADMIN NÃO TENHA UNIDADE DE ACESSO SELECIONADA, VERIFICA SE HÁ UNIDADES SELECIONADAS PARA ELE E PEGA A PRIMEIRA, CASO NÃO HAJA, CONFIGURA A UNIDADE DE ACESSO PARA TESTES COMO SELECIONADA
                        if (userIdentity.UnidadeAcessoSelecionada == null || userIdentity.UnidadeAcessoSelecionada == Guid.Empty)
                        {

                            var usuario = await _usuarioRepository.GetByIdWithPermission(user.Id);
                            IList<Guid> idGrupos = new List<Guid>();
                            foreach (var grupo in usuario.GrupoAspNetUsers) idGrupos.Add(grupo.GrupoId);
                            var gruposUnidadesAcesso = await _grupoUnidadeAcessoRepository.GetByGruposId(idGrupos.ToArray());
                            UnidadeAcesso unidadeAcessoParaTestes = await _unidadeAcessoRepository.GetUnidadeAcessoParaTestes();
                            if (gruposUnidadesAcesso == null || gruposUnidadesAcesso?.Count == 0)
                            {
                                user.UnidadeAcessoSelecionada = unidadeAcessoParaTestes.Id;
                            }
                            else
                            {
                                var unidadesAcesso = await _unidadeAcessoRepository.Get(gruposUnidadesAcesso.Select(gc => gc.UnidadeAcessoId).ToArray());
                                if (unidadesAcesso == null || unidadesAcesso?.Count == 0)
                                {
                                    user.UnidadeAcessoSelecionada = unidadeAcessoParaTestes.Id;
                                }
                                else
                                {
                                    unidadesAcesso = unidadesAcesso.OrderBy(gc => gc.Nome).ToList();
                                    user.UnidadeAcessoSelecionada = unidadesAcesso.FirstOrDefault()?.Id ?? unidadeAcessoParaTestes.Id;
                                }
                            }

                        }
                        
                        await _usuarioRepository.UpdateAsync(user);
                        userIdentity.Ativo = true;
                        userIdentity.Deletado = false;

                    }

                    if (userIdentity == null || userIdentity.Deletado || !userIdentity.Ativo)
                    {

                        loginResponse.Message = "Login incorreto ou usuário inativo.";
                        result.WithError("Login incorreto ou usuário inativo.");
                        result.Value = loginResponse;
                        return result;

                    }

                    //CASO O USUÁRIO NÃO TENHA UNIDADE DE ACESSO SELECIONADA, VERIFICA SE HÁ UNIDADES SELECIONADAS PARA ELE E PEGA A PRIMEIRA DA ORDEM POR NOME, CASO CONTRÁRIO, INVALIDA O LOGIN
                    if (userIdentity.UnidadeAcessoSelecionada == null || userIdentity.UnidadeAcessoSelecionada == Guid.Empty)
                    {

                        AspNetUsers user = await _usuarioRepository.GetById(userIdentity.Id);
                        string message = "Nenhum dos perfis do usuário possui unidade de acesso selecionada.";
                        var usuario = await _usuarioRepository.GetByIdWithPermission(user.Id);
                        IList<Guid> idGrupos = new List<Guid>();
                        foreach (var grupo in usuario.GrupoAspNetUsers) idGrupos.Add(grupo.GrupoId);
                        var gruposUnidadesAcesso = await _grupoUnidadeAcessoRepository.GetByGruposId(idGrupos.ToArray());
                        if (gruposUnidadesAcesso == null || gruposUnidadesAcesso?.Count == 0)
                        {                            
                            loginResponse.Message = message;
                            result.WithError(message);
                            result.Value = loginResponse;
                            return result;
                        }
                        else
                        {
                            var unidadesAcesso = await _unidadeAcessoRepository.Get(gruposUnidadesAcesso.Select(gc => gc.UnidadeAcessoId).ToArray());
                            if (unidadesAcesso == null || unidadesAcesso?.Count == 0)
                            {
                                loginResponse.Message = message;
                                result.WithError(message);
                                result.Value = loginResponse;
                                return result;
                            }
                            else
                            {
                                unidadesAcesso = unidadesAcesso.OrderBy(gc => gc.Nome).ToList();
                                user.UnidadeAcessoSelecionada = unidadesAcesso.First().Id;
                                await _usuarioRepository.UpdateAsync(user);
                            }
                        }

                    }

                    var refreshTokenLogin = await _usuarioRepository.GetRefreshTokenByAspNetUsersId(userIdentity.Id);
                    var credenciaisValidas = await _accessManager.ValidateCredentials(userIdentity, request.Password);
                    var qtd = _environmentVariables.QuantidadeLogin ?? _appSettings.QuantidadeLogin ?? "5";

                    if (credenciaisValidas && userIdentity.Deletado == false && userIdentity.QuantidadeLogin < Convert.ToInt32(qtd))
                    {
                        //Utiliza o loginResponse criado no inicio e modifica.
                        loginResponse = await Login(userIdentity, refreshTokenLogin);
                    }
                    else
                    {
                        if (userIdentity.QuantidadeLogin >= 5)
                        {
                            //Utiliza o loginResponse criado no inicio com falha, alterando a mensagem.
                            loginResponse.Message =
                                "Você ultrapassou o limite de tentativas, o seu usuário foi bloqueado!";
                            result.WithError(loginResponse.Message);
                        }
                        else
                        {
                            userIdentity.QuantidadeLogin += 1;
                            await _userManager.UpdateAsync(userIdentity);
                            //Utiliza o loginResponse criado no inicio com falha.
                        }
                    }


                    break;
                case "refresh_token":

                    var userIdentityRefreshToken = await _userManager.FindByIdAsync(request.UserName);

                    if (userIdentityRefreshToken == null) return new Result<LoginResponse>(loginResponse);

                    var refreshToken = await _usuarioRepository.GetRefreshTokenByAspNetUsersId(Guid.Parse(request.UserName));

                    if (refreshToken.RefreshToken == request.Password && refreshToken.ExpiredTime > DateTime.Now)
                    {
                        //Utiliza o loginResponse criado no inicio e modifica.
                        loginResponse = await Login(userIdentityRefreshToken, refreshToken);
                    }

                    break;

            }

            if (!loginResponse.Authenticated)
            {

                if (request.GrantTypes == "refresh_token")
                {

                    loginResponse = new LoginResponse()
                    {
                        Message = "Sua sessão expirou. Você será redirecionado para o login."
                    };
                    result.WithError("Sua sessão expirou. Você será redirecionado para o login.");

                }
                else
                {

                    loginResponse = new LoginResponse()
                    {
                        Message = "Login incorreto."
                    };
                    result.WithError("Login incorreto.");

                }

            }

            result.Value = loginResponse;
            return result;

        }

        public async Task<LoginResponse> Login(
            ApplicationUser user,
            AspNetUsersRefreshToken aspNetUsersRefreshToken = null
        )
        {

            var loginResponse = new LoginResponse();
            var primeiroLogin = user.PrimeiroLogin;

            if (user.QuantidadeLogin > 0 || user.PrimeiroLogin)
            {

                user.PrimeiroLogin = false;
                user.QuantidadeLogin = 0;
                await _userManager.UpdateAsync(user);

            }

            var listaAcessoUsuarios = await _permissaoUsuarioRepository.Get(user.Id);
            var listaAcessoGrupo = new List<PermissaoGrupo>();
            var usuario = await _usuarioRepository.GetByIdWithPermission(user.Id);

            foreach (var grupo in usuario.GrupoAspNetUsers) listaAcessoGrupo.AddRange(grupo.Grupo.PermissaoGrupo);

            loginResponse = new LoginResponse
            {
                Id = user.Id,
                Authenticated = true,
                PrimeiroLogin = primeiroLogin,
                TermoUso = user.TermoUso,
                Nome = user.Nome,
                Message = "Autenticado com sucesso!",
                Token = _accessManager.GenerateToken(user, listaAcessoUsuarios, listaAcessoGrupo, _tokenTimeApp)
            };

            var tokenName = "Backoffice";

            await _tokenManager.ActivateAsync(user.Id.ToString(), JsonConvert.SerializeObject(loginResponse.Token));
            await _userManager.RemoveAuthenticationTokenAsync(user, "Token", tokenName);
            await _userManager.SetAuthenticationTokenAsync(user, "Token", tokenName, loginResponse.Token.AccessToken);

            var tempoRefreshTokenExpired = Convert.ToInt32(_environmentVariables.TokenSeconds ?? _appSettings.TokenSeconds) * 2;

            if (aspNetUsersRefreshToken != null)
            {

                aspNetUsersRefreshToken.RefreshToken = loginResponse.Token.RefreshToken;
                aspNetUsersRefreshToken.IssuedTime = DateTime.Now;
                aspNetUsersRefreshToken.ExpiredTime = DateTime.Now.AddSeconds(tempoRefreshTokenExpired);

                await _usuarioRepository.UpdateRefreshToken(aspNetUsersRefreshToken);

            }
            else
            {

                await _usuarioRepository.AddRefreshToken(new AspNetUsersRefreshToken
                {
                    AspNetUsersId = user.Id,
                    RefreshToken = loginResponse.Token.RefreshToken,
                    IssuedTime = DateTime.Now,
                    ExpiredTime = DateTime.Now.AddSeconds(tempoRefreshTokenExpired)
                });

            }

            IList<string> menus = new List<string>();
            IList<string> menusSubItens = new List<string>();

            IList<Guid> idGrupos = new List<Guid>();
            foreach (var grupo in usuario.GrupoAspNetUsers) idGrupos.Add(grupo.GrupoId);
            var modulosByPerfilId = await _grupoModuloRepository.GetByGruposId(idGrupos.ToArray());
            menus = modulosByPerfilId.Select(gc => gc.Modulo).ToArray();

            IEnumerable<PermissaoUsuario> permissoesXUsuario = await _permissaoUsuarioRepository.Get(usuario.Id);
            IList<PermissaoGrupo> permissoesXGrupo = await _permissaoGrupoRepository.Get(idGrupos.ToArray());
            IList<Permissao> permissoesUsuario = await _permissaoRepository.Get(permissoesXUsuario.Select(gc => gc.PermissaoId).ToArray());
            IList<Permissao> permissoesGrupo = await _permissaoRepository.Get(permissoesXGrupo.Select(gc => gc.PermissaoId).ToArray());
            foreach (var item in permissoesUsuario)
            {
                if (!menusSubItens.Contains(item.Nome)) menusSubItens.Add(item.Nome);
            }
            foreach (var item in permissoesGrupo)
            {
                if (!menusSubItens.Contains(item.Nome)) menusSubItens.Add(item.Nome);
            }

            loginResponse.Menus = menus.ToArray();
            loginResponse.MenusSubItens = menusSubItens.ToArray();

            UnidadeAcesso unidadeAcesso = await _unidadeAcessoRepository.GetById((usuario.UnidadeAcessoSelecionada ?? Guid.Empty));
            var gruposUnidadesAcesso = await _grupoUnidadeAcessoRepository.GetByGruposId(idGrupos.ToArray());
            var unidadesAcesso = await _unidadeAcessoRepository.Get(gruposUnidadesAcesso.Select(gc => gc.UnidadeAcessoId).ToArray());
            IList<KeyValuePair<string, string>> unidadesAcessoKeyValuePair = new List<KeyValuePair<string, string>>();
            foreach (var item in unidadesAcesso) unidadesAcessoKeyValuePair.Add(new KeyValuePair<string, string>(item.Id.ToString().ToUpper(), item.Nome));
            loginResponse.SelectedAccessUnitId = unidadeAcesso.Id.ToString().ToUpper();
            loginResponse.SelectedAccessUnitName = unidadeAcesso.Nome;
            loginResponse.AvailableAccessUnits = unidadesAcessoKeyValuePair.ToArray();

            return loginResponse;

        }

    }

}