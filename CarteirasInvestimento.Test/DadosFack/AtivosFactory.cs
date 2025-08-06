using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Test
{
    public class AtivosFactory
    {
        public static List<Ativo> DadosListaAtivos()
        {
            return new List<Ativo>
            {
                new Ativo
                {
                    Id = 1,
                    Nome = "XPBR31",
                    Tipo = TipoEnum.ACAO,
                    Quantidade = 10,
                    PrecoUnitario = 18.20m,
                    Codigo = "XPBR3e"
                },
                new Ativo
                {
                    Id = 2,
                    Nome = "FII03",
                    Tipo = TipoEnum.FII,
                    Quantidade = 15,
                    PrecoUnitario = 12.50m,
                    Codigo = "FII03"
                }
            };
        }
        public static Ativo DadosAtivos()
        {
            return new Ativo
            {
                Id = 1,
                Nome = "XPBR31",
                Tipo = TipoEnum.ACAO,
                Quantidade = 10,
                PrecoUnitario = 18.20m,
                Codigo = "XPBR3e"
            };
        }
    }
}
