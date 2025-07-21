using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Repository
{
    public class AtivoMockFactory
    {
        public static List<Ativo> GetAtivosMock()
        {
            return new List<Ativo>
            {
                new Ativo
                {
                    Id = 1,
                    Nome = "Ação do Banco XP",
                    Tipo = TipoEnum.ACAO,
                    Quantidade = 20,
                    PrecoUnitario = 18.20m,
                    CarteiraId = 123,
                    Codigo = "XPBR31",
                    Carteira = new Carteira
                    {
                        Id = 2,
                        ClienteId = 123,
                        
                    }
                },
                new Ativo
                {
                    Id = 2,
                    Nome = "CDB do Banco XP",
                    Tipo = TipoEnum.CDB,
                    Quantidade = 10,
                    Codigo = "XPBR31",
                    PrecoUnitario = 150.00m,
                    CarteiraId = 123,
                    Carteira = new Carteira
                    {
                        Id = 3,
                        ClienteId = 123
                    }
                },
                new Ativo
                {
                    Id = 4,
                    Nome = "FII do Banco XP",
                    Tipo = TipoEnum.FII,
                    Quantidade = 30,
                    Codigo = "FFIX36",
                    PrecoUnitario = 32.80m,
                    CarteiraId = 123,
                    Carteira = new Carteira
                    {
                        Id = 2,
                        ClienteId = 123
                    }
                },
            };
        }
    }
}

