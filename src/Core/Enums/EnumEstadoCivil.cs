using System.ComponentModel;

namespace Core.Enums
{
    public enum EnumEstadoCivil
    {
        [Description("Solteiro")]
        Solteiro = 0,
        [Description("Casado")]
        Casado = 1,
        [Description("Viúvo")]
        Viuvo = 2,
        [Description("Divorciado")]
        Divorciado = 3,
        [Description("Outros")]
        Outros = 4
    }
}
