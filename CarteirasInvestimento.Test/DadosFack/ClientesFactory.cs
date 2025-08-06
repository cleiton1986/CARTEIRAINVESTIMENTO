using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Test.DadosFack
{
    public class ClientesFactory
    {
        public static List<Cliente> DadosListaClientes()
        {
            return new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Nome = "João da Silva",
                    Email = "joao@gmail.com",
                    Telefone = "11987654321",
                    Logadouro = "Rua A",
                    Numero = 123,
                    Cep = "12345678",
                    Bairro = "Bairro A",
                    Complemento = "Apto 1",
                    DataNascimento = new DateTime(1990, 1, 1)

                },
                new Cliente
                {
                    Id = 2,
                    Nome = "Maria da Silva",
                    Email = "maria@gmail.com",
                    Telefone = "11995654344",
                    Logadouro = "Rua B",
                    Numero = 123,
                    Cep = "12399698",
                    Bairro = "Bairro B",
                    Complemento = "Apto 2",
                    DataNascimento = new DateTime(1991, 1, 1)

                },
            };
        }
        public static Cliente DadosCliente()
        {
            return new Cliente
            {
                Id = 1,
                Nome = "João da Silva",
                Email = "joao@gmail.com",
                Telefone = "11987654321",
                Logadouro = "Rua A",
                Numero = 123,
                Cep = "05833-111",
                Bairro = "Bairro A",
                Complemento = "Apto 1",
                DataNascimento = new DateTime(1990, 1, 1)
            };
        }

        public static ClienteCadastroView DadosCadastroCliente()
        {
            return new ClienteCadastroView
            {
                Nome = "João da Silva",
                Email = "joao@gmail.com",
                Telefone = "11987654321",
                Logadouro = "Rua A",
                Numero = 123,
                Cep = "05833-111",
                Bairro = "Bairro A",
                Complemento = "Apto 1",
                DataNascimento = "05/08/2000"
            };
        }
        public static ClienteEditarView DadosEditarCliente()
        {
            return new ClienteEditarView
            {
                Id = 1,
                Nome = "João da Silva",
                Email = "joao@gmail.com",
                Telefone = "11987654321",
                Logadouro = "Rua A",
                Numero = 123,
                Cep = "05833-111",
                Bairro = "Bairro A",
                Complemento = "Apto 1",
                DataNascimento = "05/08/2000"
            };
        }
    }
}







