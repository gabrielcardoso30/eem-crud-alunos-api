using Microsoft.Extensions.DependencyInjection;
using Core.Enums.Security;
using Core.Security.Policy;

namespace Web.Extension
{

    public static class PolicyExtension
    {

        public static IServiceCollection AddPolicy(this IServiceCollection services)
        {

            services.AddAuthorization(options =>
            {

                /****  Permissões do Controller de Unidade de Acesso ****/
                options.AddPolicy(EnumPermissao.CadastrarUnidadeAcesso.ToString(),
                    policy => policy.Requirements.Add(new PolicyRequirement(EnumPermissao.CadastrarUnidadeAcesso.ToString())));
                options.AddPolicy(EnumPermissao.AtualizarUnidadeAcesso.ToString(),
                    policy => policy.Requirements.Add(new PolicyRequirement(EnumPermissao.AtualizarUnidadeAcesso.ToString())));
                options.AddPolicy(EnumPermissao.DeletarUnidadeAcesso.ToString(),
                    policy => policy.Requirements.Add(new PolicyRequirement(EnumPermissao.DeletarUnidadeAcesso.ToString())));
                options.AddPolicy(EnumPermissao.ConsultarUnidadeAcesso.ToString(),
                    policy => policy.Requirements.Add(new PolicyRequirement(EnumPermissao.ConsultarUnidadeAcesso.ToString())));

                /****  Permissões do Controller de Usuário ****/
                options.AddPolicy(EnumPermissao.CadastrarUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.CadastrarUsuario.ToString())));
                options.AddPolicy(EnumPermissao.AtualizarUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.AtualizarUsuario.ToString())));
                options.AddPolicy(EnumPermissao.DeletarUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.DeletarUsuario.ToString())));
                options.AddPolicy(EnumPermissao.ConsultarUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.ConsultarUsuario.ToString())));
                options.AddPolicy(EnumPermissao.ResetarSenhaUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.ResetarSenhaUsuario.ToString())));
                options.AddPolicy(EnumPermissao.DesbloquearUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.DesbloquearUsuario.ToString())));
                options.AddPolicy(EnumPermissao.AssociarUsuarioGrupo.ToString(),
                    policy => policy.Requirements.Add(new PolicyRequirement(EnumPermissao.AssociarUsuarioGrupo.ToString())));

                /****  Permissões do Controller de Grupo ****/
                options.AddPolicy(EnumPermissao.CadastrarGrupo.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.CadastrarGrupo.ToString())));
                options.AddPolicy(EnumPermissao.AtualizarGrupo.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.AtualizarGrupo.ToString())));
                options.AddPolicy(EnumPermissao.DeletarGrupo.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.DeletarGrupo.ToString())));
                options.AddPolicy(EnumPermissao.ConsultarGrupo.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.ConsultarGrupo.ToString())));

                /****  Permissões do Controller de Permissão do Grupo ****/
                options.AddPolicy(EnumPermissao.CadastrarPermissaoGrupo.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.CadastrarPermissaoGrupo.ToString())));
                options.AddPolicy(EnumPermissao.DeletarPermissaoGrupo.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.DeletarPermissaoGrupo.ToString())));
                options.AddPolicy(EnumPermissao.ConsultarPermissoesGrupo.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.ConsultarPermissoesGrupo.ToString())));

                /****  Permissões do Controller de Permissão do Usuário ****/
                options.AddPolicy(EnumPermissao.CadastrarPermissaoUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.CadastrarPermissaoUsuario.ToString())));
                options.AddPolicy(EnumPermissao.DeletarPermissaoUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.DeletarPermissaoUsuario.ToString())));
                options.AddPolicy(EnumPermissao.ConsultarPermissoesUsuario.ToString(),
                    policy => policy.Requirements.Add( new PolicyRequirement(EnumPermissao.ConsultarPermissoesUsuario.ToString())));

                    

            });

            return services;

        }
        
    }

}