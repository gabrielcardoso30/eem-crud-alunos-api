using System.ComponentModel;

namespace Core.Enums.Security
{

    public enum EnumPermissao
    {

        [Description("ConsultarUnidadeAcesso")] ConsultarUnidadeAcesso,
        [Description("CadastrarUnidadeAcesso")] CadastrarUnidadeAcesso,
        [Description("AtualizarUnidadeAcesso")] AtualizarUnidadeAcesso,
        [Description("DeletarUnidadeAcesso")] DeletarUnidadeAcesso,

        [Description("ConsultarUsuario")] ConsultarUsuario,
        [Description("CadastrarUsuario")] CadastrarUsuario,
        [Description("AtualizarUsuario")] AtualizarUsuario,
        [Description("DeletarUsuario")] DeletarUsuario,
        [Description("ResetarSenhaUsuario")] ResetarSenhaUsuario,
        [Description("DesbloquearUsuario")] DesbloquearUsuario,

        [Description("AssociarUsuarioGrupo")] AssociarUsuarioGrupo,
        
        [Description("ConsultarGrupo")] ConsultarGrupo,
        [Description("CadastrarGrupo")] CadastrarGrupo, 
        [Description("AtualizarGrupo")] AtualizarGrupo,
        [Description("DeletarGrupo")] DeletarGrupo,
        
        [Description("ConsultarPermissoesGrupo")] ConsultarPermissoesGrupo, 
        [Description("CadastrarPermissaoGrupo")] CadastrarPermissaoGrupo, 
        [Description("DeletarPermissaoGrupo")] DeletarPermissaoGrupo,
        
        [Description("ConsultarPermissoesUsuario")] ConsultarPermissoesUsuario,
        [Description("CadastrarPermissaoUsuario")] CadastrarPermissaoUsuario,
        [Description("DeletarPermissaoUsuario")] DeletarPermissaoUsuario,

        [Description("ConsultarResponsavel")] ConsultarResponsavel,
        [Description("CadastrarResponsavel")] CadastrarResponsavel,
        [Description("AtualizarResponsavel")] AtualizarResponsavel,
        [Description("DeletarResponsavel")] DeletarResponsavel,

        [Description("ConsultarAluno")] ConsultarAluno,
        [Description("CadastrarAluno")] CadastrarAluno,
        [Description("AtualizarAluno")] AtualizarAluno,
        [Description("DeletarAluno")] DeletarAluno,


    }

}