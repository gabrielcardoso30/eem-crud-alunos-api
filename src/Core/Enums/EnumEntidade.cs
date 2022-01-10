using System.ComponentModel;

namespace Core.Enums
{
    public enum EnumEntidade
    {
        [Description("Empresa")] Empresa,
        [Description("LogIntegracao")] LogIntegracao,
        [Description("ParametroSistema")] ParametroSistema,
        [Description("GrupoAspNetUsers")] GrupoAspNetUsers,
        [Description("Termo")] Termo,
        [Description("PermissaoGrupo")] PermissaoGrupo,
        [Description("Notificacao")] Notificacao,
        [Description("Grupo")] Grupo,
        [Description("Favorito")] Favorito,
        [Description("AspNetUsers")] AspNetUsers
    }
}