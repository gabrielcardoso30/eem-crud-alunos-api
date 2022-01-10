using AutoMapper;
using Core.Entities.Gerencial;
using Core.Entities.Security;
using Core.Models.Responses.Gerencial;
using Core.Models.Responses.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Enums.Gerencial;
using Core.Helpers;

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

            CreateMap<Aluno, AlunoResponse>()
            .AfterMap((src, dest) =>
            {

                IDictionary<string, string> dic = Enum.GetValues(typeof(EnumSegmento)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
                dest.Id = src.Id != null ? src.Id.ToString().ToUpper() : String.Empty;
                dest.DataNascimento = src.DataNascimento != null ? Convert.ToDateTime(src.DataNascimento).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : String.Empty;
                dest.DataNascimentoFormatada = src.DataNascimento != null ? Convert.ToDateTime(src.DataNascimento).ToString("dd/MM/yyyy HH:mm:ss") : String.Empty;
                dest.SegmentoDescricao = src.Segmento != null ? dic.Where(gc => gc.Value == src.Segmento).FirstOrDefault().Key : String.Empty;

            });

            CreateMap<Responsavel, ResponsavelResponse>()
            .AfterMap((src, dest) =>
            {

                IDictionary<string, string> dic = Enum.GetValues(typeof(EnumParentesco)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
                dest.Id = src.Id != null ? src.Id.ToString().ToUpper() : String.Empty;
                dest.DataNascimento = src.DataNascimento != null ? Convert.ToDateTime(src.DataNascimento).ToString("yyyy-MM-ddTHH:mm:ss.fffZ") : String.Empty;
                dest.DataNascimentoFormatada = src.DataNascimento != null ? Convert.ToDateTime(src.DataNascimento).ToString("dd/MM/yyyy HH:mm:ss") : String.Empty;
                dest.ParentescoDescricao = src.Parentesco != null ? dic.Where(gc => gc.Value == src.Parentesco).FirstOrDefault().Key : String.Empty;

            });

        }

    }

}