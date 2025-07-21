using System.ComponentModel;

namespace CarteirasInvestimento.DataAcess.Entity
{
    public enum TipoEnum
    {
        [Description("CDB")]
        CDB = 1,
        [Description("FII")]
        FII = 2,
        [Description("AÇÃO")]
        ACAO = 3,
    }
}
