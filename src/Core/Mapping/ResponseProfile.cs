using AutoMapper;
using Core.Entities.Security;
using Core.Models.Responses.Security;
using System;
using System.Linq;

namespace Core.Mapping
{

    public class ResponseProfile : Profile
    {

        public ResponseProfile()
        {

            CreateMap<ApplicationUser, AspNetUsers>();
            CreateMap<PermissaoGrupo, PermissaoGrupoResponse>();
            CreateMap<PermissaoUsuario, PermissaoUsuarioResponse>();

            CreateMap<Grupo, GrupoResponse>()
                .ForMember(u => u.Permissoes, ua => ua.MapFrom(m => m.PermissaoGrupo.Select(x => x.Permissao)));
            CreateMap<AspNetUsers, UsuarioResponse>()
                .ForMember(u => u.Inativo, ua => ua.MapFrom(m => !m.Ativo))
                .ForMember(u => u.Telefone, ua => ua.MapFrom(m => m.PhoneNumber))
                .ForMember(u => u.TipoUsuario, ua => ua.MapFrom(m => m.TipoUsuario))
                .ForMember(u => u.Login, ua => ua.MapFrom(m => m.UserName));

            CreateMap<Permissao, PermissaoResponse>();
            CreateMap<Auditoria, AuditoriaResponse>()
                .ForMember(u => u.Acao, ua => ua.MapFrom(m => m.EntityState))
                .ForMember(u => u.Usuario, ua => ua.MapFrom(m => m.AspNetUsers));

            CreateMap<GrupoAspNetUsers, GrupoAspNetUsersResponse>();
            CreateMap<ParametroSistema, ParametroSistemaResponse>();

            CreateMap<UnidadeAcesso, UnidadeAcessoResponse>()
            .AfterMap((src, dest) =>
            {

                dest.Id = src.Id != null ? src.Id.ToString().ToUpper() : String.Empty;

            });

        }

    }

}