using AutoMapper;
using Core.Commands.Security;
using Core.Entities.Security;
using Core.Models.Request.Security;
using System;

namespace Core.Mapping
{

    public class CommandProfile : Profile
    {

        public CommandProfile()
        {

            CreateMap<CreateGrupoCommand, Grupo>();
            CreateMap<UpdateGrupoCommand, Grupo>();
            CreateMap<CreatePermissaoGrupoCommand, PermissaoGrupo>();

            CreateMap<CreateUsuarioCommand, AspNetUsers>()
                .ForMember(u => u.PhoneNumber, ua => ua.MapFrom(m => m.Telefone))
                ;
            CreateMap<UpdateUsuarioCommand, AspNetUsers>()
                .ForMember(u => u.Email, ua => ua.MapFrom(m => m.Email))
                .ForMember(u => u.PhoneNumber, ua => ua.MapFrom(m => m.Telefone));

            CreateMap<UpdateUsuarioCommand, ApplicationUser>()
                .ForMember(u => u.Email, ua => ua.MapFrom(m => m.Email))
                .ForMember(u => u.PhoneNumber, ua => ua.MapFrom(m => m.Telefone))
                .ForMember(u => u.TelefoneResidencial, ua => ua.MapFrom(m => m.TelefoneResidencial))
                .ForMember(u => u.UserName, ua => ua.MapFrom(m => m.Login));

            CreateMap<UpdateUsuarioStatusCommand, AspNetUsers>();
            CreateMap<UpdateUsuarioStatusCommand, ApplicationUser>();

            CreateMap<CreateGrupoAspNetUsersCommand, GrupoAspNetUsers>();
            CreateMap<CreateParametroSistemaCommand, ParametroSistema>();
            CreateMap<UpdateParametroSistemaCommand, ParametroSistema>();

            CreateMap<CreateUnidadeAcessoRequest, UnidadeAcesso>()
            .AfterMap((src, dest) =>
             {
                 dest.DataCriacao = DateTime.Now;
                 dest.Deletado = false;
             });
            CreateMap<UpdateUnidadeAcessoRequest, UnidadeAcesso>();

        }

    }

}