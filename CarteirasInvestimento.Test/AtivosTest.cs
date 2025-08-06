using AutoFixture;
using AutoMapper;
using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Infra;
using CarteirasInvestimento.Repository.Interfaces;
using Moq;

namespace CarteirasInvestimento.Test
{
    public class AtivosTest
    {
        private readonly Fixture _fixture;
        public readonly Mock<IAtivoAppServe> _ativoAppServe;
        public readonly Mock<IAtivoRepository> _ativoRepository;
        public readonly IMapper _mapper;

        public AtivosTest()
        {
            _fixture = new Fixture();
            _ativoAppServe = new Mock<IAtivoAppServe>();
            _ativoRepository = new Mock<IAtivoRepository>();

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Ativo, AtivoView>();
                cfg.CreateMap<AtivoView, Ativo>();
            });

            _mapper = config.CreateMapper();
           
        }


        [Fact(DisplayName = "Buscar todos os ativos")]
        [Trait("Ativos", "Ativos testes")]
        public async Task BuscarTodos_AtivosExistente_RetornaAtivosCorreta()
        {

            var listaAtivos = AtivosFactory.DadosListaAtivos();
            _ativoRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listaAtivos);


            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            var resultado = await ativoAppService.GetAllAsync();

            Assert.NotNull(resultado);
        }

        [Fact(DisplayName = "Ativo tem que retornar busca a por Id")]
        [Trait("Ativos", "Ativos testes")]
        public async Task BuscarPorId_AtivosExistente_RetornaAtivoCorreto()
        {
            var ativo = AtivosFactory.DadosAtivos();

            _ativoRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(ativo);
            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            var resultado = await ativoAppService.GetByIdAsync(1);
            resultado.TipoId = 3;

            Assert.NotNull(resultado);           
            Assert.Equal(ativo.Nome, resultado.Nome);
            Assert.Equal(ativo.Tipo, (TipoEnum)resultado.TipoId);
            Assert.Equal(ativo.Codigo, resultado.Codigo);
            Assert.Equal(ativo.Quantidade, resultado.Quantidade);
            Assert.Equal(ativo.PrecoUnitario, resultado.PrecoUnitario);

           _ativoRepository.Verify(x => x.GetByIdAsync(1), Times.Once);
     
        }
        [Fact(DisplayName = "Ativo deve ser adicionado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task AddAtivoAsync_AdicionarAtivo_Correto()
        {
            var ativo = AtivosFactory.DadosAtivos();
            var ativoView = _mapper.Map<AtivoView>(ativo);
            ativoView.TipoId = (int)TipoEnum.ACAO;

            //var mapperMock = new Mock<IMapper>();
            //mapperMock.Setup(m => m.Map<Ativo, AtivoView>(It.IsAny<Ativo>()));

            _ativoRepository.Setup(x => x.AddAtivoAsync(ativo));
            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            await ativoAppService.AddAtivoAsync(ativoView);
            var retornoNotification = Notification.GetErrors();


            Assert.True(!retornoNotification.Any());
            _ativoRepository.Verify(x => x.AddAtivoAsync(It.IsAny<Ativo>()), Times.Once());

        }

        [Fact(DisplayName = "Ativo não deve ser adicionado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task AddAtivoAsync_AdicionarAtivo_InCorreto()
        {
            var ativo = AtivosFactory.DadosAtivos();
            // var ativoView = _mapper.Map<AtivoView>(ativo);
            var ativoView = new AtivoView { };
            //Notification.Notify($"Erro ao cadastrar ativo. : {ex.Message} ", "AddAtivoAsync");

            // _notification.Setup(m => m.Sucesso).Returns(false);
            _ativoRepository.Setup(x => x.AddAtivoAsync(ativo));
            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            await ativoAppService.AddAtivoAsync(ativoView);

            var retornoNotification = Notification.GetErrors();

           // var exception = await Assert.ThrowsAsync<NotificationException>(() => ativoAppService.AddAtivoAsync(ativoView));

           // Assert.Equal(exception.Message, $"Erro ao cadastrar ativo. : {exception.Message} ");

            var validarNome = retornoNotification.Contains("Nome deve ser preenchido.");
            var validarQuantidade = retornoNotification.Contains("Quantidade deve ser maior que zero.");
            var validarTipo = retornoNotification.Contains("Tipo do ativo deve ser preenchido.");
            var validarCodigo = retornoNotification.Contains("Código deve ser maior que zero.");
            var validarPreco = retornoNotification.Contains("Preço unitário deve ser preenchido.");

            Assert.True(retornoNotification.Any());
            Assert.True(validarNome);
            Assert.True(validarQuantidade);
            Assert.True(validarTipo);
            Assert.True(validarCodigo);
            Assert.True(validarPreco);


            //_ativoRepository.Verify(x => x.AddAtivoAsync(It.IsAny<Ativo>()), Times.Once());

        }
      
        [Fact(DisplayName = "Ativo deve ser atualizado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task UpdateAtivoAsync_UpdateAtivo_Correto()
        {
            var ativo = AtivosFactory.DadosAtivos();
            var retornoNotification = Notification.GetErrors();

            _ativoRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(ativo); 
            _ativoRepository.Setup(x => x.UpdateAtivoAsync(ativo));

            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            var ativoView = await ativoAppService.GetByIdAsync(1);

            ativoView.TipoId = (int)TipoEnum.ACAO;
            await ativoAppService.UpdateAtivoAsync(ativoView);
           

            Assert.True(!retornoNotification.Any());
            _ativoRepository.Verify(x => x.UpdateAtivoAsync(It.IsAny<Ativo>()), Times.Once());

        }

        [Fact(DisplayName = "Ativo não deve ser atualizado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task UpdateAtivoAsync_UpdateAtivo_InCorreto()
        {
            var ativo = AtivosFactory.DadosAtivos();
           

            _ativoRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(ativo);
            _ativoRepository.Setup(x => x.UpdateAtivoAsync(ativo));

            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            var ativoView = await ativoAppService.GetByIdAsync(1);

            ativoView.TipoId = (int)TipoEnum.ACAO;

            ativoView.Id = 0; // Simulando um Id inválido
            await ativoAppService.UpdateAtivoAsync(ativoView);

            var retornoNotification = Notification.GetErrors();
            var validacaoMensagemId = retornoNotification.Contains($"Ativo com Id: {ativoView.Id} não foi encontrado.");
         
            Assert.True(retornoNotification.Any());
            Assert.True(validacaoMensagemId);

        }

        [Fact(DisplayName = "Ativo deve ser deletado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task DeleteAtivoAsync_DeleteAtivo_Correto()
        {
            var ativo = AtivosFactory.DadosAtivos();

            _ativoRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(ativo);
            _ativoRepository.Setup(x => x.DeleteAtivoAsync(ativo));

            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            var ativoView = await ativoAppService.GetByIdAsync(1);

            ativoView.TipoId = (int)TipoEnum.ACAO;

            await ativoAppService.DeleteAtivoAsync(ativoView.Id);

            var retornoNotification = Notification.GetErrors();
     
            Assert.True(!retornoNotification.Any());
            _ativoRepository.Verify(x => x.DeleteAtivoAsync(It.IsAny<Ativo>()), Times.Once());

        }

        [Fact(DisplayName = "Ativo não deve ser deletado com sucesso")]
        [Trait("Ativos", "Ativos testes")]
        public async Task DeleteAtivoAsync_DeleteAtivo_InCorreto()
        {
            var ativo = AtivosFactory.DadosAtivos();

            _ativoRepository.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(ativo);
            _ativoRepository.Setup(x => x.DeleteAtivoAsync(ativo));

            var ativoAppService = new AtivoAppServe(_ativoRepository.Object, _mapper);
            var ativoView = await ativoAppService.GetByIdAsync(1);

            ativoView.TipoId = (int)TipoEnum.ACAO;
            ativoView.Id = 0; // Simulando um Id inválido
            await ativoAppService.DeleteAtivoAsync(ativoView.Id);

            var retornoNotification = Notification.GetErrors();
            var validacaoMensagemId = retornoNotification.Contains($"Id do Ativo é obrigatório.");

            Assert.True(retornoNotification.Any());
            Assert.True(validacaoMensagemId);

        }
    }
}


