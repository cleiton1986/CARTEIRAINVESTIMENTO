using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Test
{
    public class CarteiraFack
    {
        public static List<Carteira> DadosListaCarteira()
        {
            return new List<Carteira>
            {
                new Carteira
                {
                    Id = 1,
                    ClienteId = 123,
                    AtivoId = 4,
                    Ativos = new List<Ativo>
                    {
                        new Ativo
                        {
                            Id = 1,
                            Nome = "XPBR31",
                            Tipo = TipoEnum.ACAO,
                            Quantidade = 10,
                            PrecoUnitario = 18.20m,
                            Codigo = "XPBR3e"
                        }
                    },
                },
                new Carteira
                {
                    Id = 2,
                    ClienteId = 456,
                    AtivoId = 5,
                    Ativos = new List<Ativo>
                    {
                        new Ativo
                        {
                            Id = 2,
                            Nome = "FII03",
                            Tipo = TipoEnum.FII,
                            Quantidade = 15,
                            PrecoUnitario = 12.50m,
                            Codigo = "FII03"
                        }
                    },
                }
            };
            
        }
        public static Carteira DadosCarteira()
        {
            return new Carteira
            {
                Id = 1,
                ClienteId = 123,
                AtivoId = 4, 
                Ativos = new List<Ativo>
                {
                    new Ativo
                    {
                        Id = 1,
                        Nome = "XPBR31",
                        Tipo = TipoEnum.ACAO,
                        Quantidade = 10,
                        PrecoUnitario = 18.20m,
                        Codigo = "XPBR3e"
                    }
                },
            };
        }
        public static Carteira DadosDeveAdicionarCarteira()
        {
            return new Carteira
            {
                ClienteId = 20,
                Ativos = new List<Ativo>
                {
                    new Ativo
                    {
                        Quantidade = 13,
                        Codigo = "FII03"
                    }
                },
            };
        }
        public static CarteiraCadastroView DadosDeveAdicionarCarteiraView()
        {
            return new CarteiraCadastroView
            {
                ClienteId = 20,
                Ativos = new List<AtivoCarteiraView>
                {
                    new AtivoCarteiraView
                    {
                        Quantidade = 13,
                        Codigo = "FII03"
                    }
                },
            };
        }

        public static Carteira DadosNaoDeveAdicionarCarteira()
        {
            return new Carteira
            {
                ClienteId = 20,
                Ativos = new List<Ativo>
                {
                    new Ativo
                    {
                        Quantidade = 13,
                        Codigo = "FII03"
                    }
                },
            };
        }
        public static CarteiraCadastroView DadosNaoDeveAdicionarCarteiraView()
        {
            return new CarteiraCadastroView
            {
                ClienteId = 21,
                Ativos = new List<AtivoCarteiraView>
                {
                    new AtivoCarteiraView
                    {
                        Quantidade = 13,
                        Codigo = "FII03"
                    }
                },
            };
        }
    }
}
