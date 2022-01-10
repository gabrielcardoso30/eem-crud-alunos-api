using System.ComponentModel;

namespace Core.Enums.Gerencial
{

    public enum EnumParentesco
    {

        [Description("Pai")]
        Pai,
        [Description("Mãe")]
        Mae,
        [Description("Irmão")]
        Irmao,
        [Description("Irmã")]
        Irma,
        [Description("Tio")]
        Tio,
        [Description("Tia")]
        Tia,
        [Description("Primo")]
        Primo,
        [Description("Prima")]
        Prima,
        [Description("Avô")]
        AvoHomem,
        [Description("Avó")]
        AvoMulher,
        [Description("Responsável Legal")]
        ResponsavelLegal,
        [Description("Outro")]
        Outro,

    }

}
