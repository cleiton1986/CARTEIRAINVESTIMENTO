using AutoFixture;
using AutoMapper;
using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.AppServer.ViewModel;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Infra;
using CarteirasInvestimento.Repository;
using CarteirasInvestimento.Repository.Interfaces;
using CarteirasInvestimento.Test.DadosFack;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CarteirasInvestimento.Test
{
    public class ClienteTest
    {
        private readonly Fixture _fixture;
        public readonly Mock<IClienteAppServe> _clienteAppServe;
        public readonly Mock<IClienteRepository> _clienteRepository;
        public readonly IMapper _mapper;
        public ClienteTest()
        {
            _fixture = new Fixture();
            _clienteRepository = new Mock<IClienteRepository>();
            _clienteAppServe = new Mock<IClienteAppServe>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cliente, ClienteEditarView>();
                cfg.CreateMap<ClienteEditarView, Cliente>();
                cfg.CreateMap<Cliente, ClienteCadastroView>();
                cfg.CreateMap<ClienteCadastroView, Cliente>();
                cfg.CreateMap<Cliente, ClienteView>();
                cfg.CreateMap<ClienteView, Cliente>();
            });

            _mapper = config.CreateMapper();
           
        }

        [Fact(DisplayName = "Buscar todos os clientes")]
        [Trait("Clientes", "Clientes testes")]
        public async Task BuscarTodos_ClientesExistente_RetornaClientesCorreta()
        {
            var listaClientes = ClientesFactory.DadosListaClientes();
            _clienteRepository.Setup(x => x.GetAllClientesAsync()).ReturnsAsync(listaClientes);

            var clienteAppService = new ClienteAppServe(_clienteRepository.Object, _mapper);
            var resultado = await clienteAppService.GetAllAsync();

            Assert.NotNull(resultado);
        }

        [Fact(DisplayName = "Cliente tem que retornar busca a por Id")]
        [Trait("Cliente", "Cliente testes")]
        public async Task BuscarPorId_ClienteExistente_RetornaClienteCorreto()
        {
            var cliente = ClientesFactory.DadosCliente();

            _clienteRepository.Setup(x => x.GetByClienteIdAsync(1)).ReturnsAsync(cliente);
            var clienteAppServe = new ClienteAppServe(_clienteRepository.Object, _mapper);
            var resultado = await clienteAppServe.GetByClienteIdAsync(1);
            var dataNascimento = cliente.DataNascimento.ConvertDateToString();


            Assert.NotNull(resultado);
            Assert.Equal(cliente.Nome, resultado.Nome);
            Assert.Equal(cliente.Email, resultado.Email);
            Assert.Equal(cliente.Cep, resultado.Cep);
            Assert.Equal(cliente.Complemento, resultado.Complemento);
            Assert.Equal(cliente.Bairro, resultado.Bairro);
            Assert.Equal(cliente.Telefone, resultado.Telefone);
            Assert.Equal(cliente.Numero, resultado.Numero);
         //   Assert.Equal(dataNascimento, resultado.DataNascimento);

            _clienteRepository.Verify(x => x.GetByClienteIdAsync(1), Times.Once);

        }

        [Fact(DisplayName = "Cliente deve ser adicionado com sucesso")]
        [Trait("Cliente", "Cliente testes")]
        public async Task AddAsync_AdicionarCliente_Correto()
        {
            var clienteView = ClientesFactory.DadosCadastroCliente();
            var cliente = _mapper.Map<Cliente>(clienteView);

            _clienteRepository.Setup(x => x.AddClienteAsync(cliente));
            var clienteAppServe = new ClienteAppServe(_clienteRepository.Object, _mapper);
            await clienteAppServe.AddAsync(clienteView);
            var retornoNotification = Notification.GetErrors();

            Assert.True(!retornoNotification.Any());
            _clienteRepository.Verify(x => x.AddClienteAsync(It.IsAny<Cliente>()), Times.Once());

        }


        [Fact(DisplayName = "Cliente não deve ser adicionado com sucesso")]
        [Trait("Cliente", "Cliente testes")]
        public async Task AddAsync_AdicionarCliente_InCorreto()
        {
            var clienteView = new ClienteCadastroView { };
            var cliente = _mapper.Map<Cliente>(clienteView);

            _clienteRepository.Setup(x => x.AddClienteAsync(cliente));
            var clienteAppServe = new ClienteAppServe(_clienteRepository.Object, _mapper);
            await clienteAppServe.AddAsync(clienteView);
            var retornoNotification = Notification.GetErrors();


            var validarNome = retornoNotification.Contains("Nome é obrigatório.");
            var validarEmail = retornoNotification.Contains("Email inválido.");
            var validarTelefone = retornoNotification.Contains("Telefone é obrigatório.");
              var validarCep = retornoNotification.Contains("Cep é obrigatório.");
            var validarNumero = retornoNotification.Contains("Número é obrigatório.");
            var validarLogadouro = retornoNotification.Contains("Logadouro é obrigatório.");
            var validarBairro = retornoNotification.Contains("Bairro é obrigatório.");
              var validarDataNascimento = retornoNotification.Contains("DataNacimento é obrigatória.");

            Assert.True(retornoNotification.Count > 0);
            Assert.True(validarNome);
           // Assert.True(validarEmail);
            Assert.True(validarTelefone);
            Assert.True(validarLogadouro);
            Assert.True(validarBairro);
            //Assert.True(validarDataNascimento);
            //Assert.True(validarCep);
            Task<ArgumentException> task = Assert.ThrowsAsync<ArgumentException>(async () => await clienteAppServe.AddAsync(clienteView));

        }

        [Fact(DisplayName = "Cliente deve ser Atualizar com sucesso")]
        [Trait("Cliente", "Cliente testes")]
        public async Task UpdateClienteAsync_AtualizarCliente_Correto()
        {
            var cliente = ClientesFactory.DadosCliente();
            var clienteView = _mapper.Map<Cliente>(cliente);

            _clienteRepository.Setup(x => x.UpdateClienteAsync(cliente));
            _clienteRepository.Setup(x => x.GetByClienteIdAsync(1)).ReturnsAsync(clienteView);

            var clienteAppServe = new ClienteAppServe(_clienteRepository.Object, _mapper);
            var clienteRetorno = await clienteAppServe.GetByClienteIdAsync(cliente.Id);

            var _cliente = _mapper.Map<Cliente>(clienteRetorno);
            var clienteEditarView = _mapper.Map<ClienteEditarView>(_cliente);
            await clienteAppServe.UpdateAsync(clienteEditarView);

            var retornoNotification = Notification.GetErrors();

            Assert.NotNull(clienteRetorno);
            Assert.True(!retornoNotification.Any());
            _clienteRepository.Verify(x => x.UpdateClienteAsync(It.IsAny<Cliente>()), Times.Once());

        }


        [Fact(DisplayName = "Cliente não deve ser Atualizar com sucesso")]
        [Trait("Cliente", "Cliente testes")]
        public async Task UpdateClienteAsync_AtualizarCliente_InCorreto()
        {
            var cliente = ClientesFactory.DadosCliente();
            var clienteView = _mapper.Map<Cliente>(cliente);

            _clienteRepository.Setup(x => x.UpdateClienteAsync(cliente));
            _clienteRepository.Setup(x => x.GetByClienteIdAsync(1)).ReturnsAsync(clienteView);

            var clienteAppServe = new ClienteAppServe(_clienteRepository.Object, _mapper);
            var clienteRetorno = await clienteAppServe.GetByClienteIdAsync(cliente.Id);

          
            await clienteAppServe.UpdateAsync(new ClienteEditarView { });

            var retornoNotification = Notification.GetErrors();

            var validarIdMensagem = retornoNotification.Contains("Cliente com Id: 0 não encontrado.");
        
            Assert.True(retornoNotification.Count > 0);
            Assert.True(validarIdMensagem);
            Task<ArgumentException> task = Assert.ThrowsAsync<ArgumentException>(async () => await clienteAppServe.UpdateAsync(new ClienteEditarView { }));

        }


        [Fact(DisplayName = "Ativo deletar deve ser deletado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task DeleteAtivoAsync_DeleteAtivo_Correto()
        {
            var cliente = ClientesFactory.DadosCliente();
            var clienteView = _mapper.Map<Cliente>(cliente);

            _clienteRepository.Setup(x => x.UpdateClienteAsync(cliente));
            _clienteRepository.Setup(x => x.GetByClienteIdAsync(1)).ReturnsAsync(clienteView);

            var clienteAppServe = new ClienteAppServe(_clienteRepository.Object, _mapper);
            var clienteRetorno = await clienteAppServe.GetByClienteIdAsync(cliente.Id);


            await clienteAppServe.DeletarAsync(1);

            var retornoNotification = Notification.GetErrors();
            var validarIdMensagem = retornoNotification.Contains("Id do cliente é obrigatório.");
            Assert.True(retornoNotification.Count <= 0);

        }

        [Fact(DisplayName = "Ativo não deve ser deletado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task DeleteAtivoAsync_DeleteAtivo_InCorreto()
        {
            var cliente = ClientesFactory.DadosCliente();
            var clienteView = _mapper.Map<Cliente>(cliente);

            _clienteRepository.Setup(x => x.UpdateClienteAsync(cliente));
            _clienteRepository.Setup(x => x.GetByClienteIdAsync(1)).ReturnsAsync(clienteView);

            var clienteAppServe = new ClienteAppServe(_clienteRepository.Object, _mapper);
            var clienteRetorno = await clienteAppServe.GetByClienteIdAsync(cliente.Id);


            await clienteAppServe.DeletarAsync(0);

            var retornoNotification = Notification.GetErrors();
            var validarIdMensagem = retornoNotification.Contains("Id do cliente é obrigatório.");

            Assert.True(retornoNotification.Any());
            Task<ArgumentException> task = Assert.ThrowsAsync<ArgumentException>(async () => await clienteAppServe.UpdateAsync(new ClienteEditarView { }));

        }
    }
}
