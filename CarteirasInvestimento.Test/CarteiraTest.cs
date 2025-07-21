using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Repository;
using CarteirasInvestimento.Repository.Interfaces;
using Moq;

namespace CarteirasInvestimento.Test
{
    public class CarteiraTest
    {
        public readonly Mock<ICarteiraRepository> _carteiraRepository;
        public readonly Mock<IAtivoRepository> _ativoRepository;
        public CarteiraTest()
        {
            _carteiraRepository = new Mock<ICarteiraRepository>();
            _ativoRepository = new Mock<IAtivoRepository>();
           
        }

        [Fact(DisplayName = "Adicionar dados da carteira")]
        [Trait("Carteira", "Carteiras testes")]
        public async Task AdicionarCarteira_DeveAdicionarCarteiraComSucesso()
        {
            var carteira = CarteiraFack.DadosDeveAdicionarCarteira();
            var carteiraView = CarteiraFack.DadosDeveAdicionarCarteiraView();


            var carteiraAppService = new CarteiraAppServe(_carteiraRepository.Object, _ativoRepository.Object);

            _carteiraRepository.Setup(x => x.GetByCarteiraIdAsync(carteira.ClienteId)).ReturnsAsync(carteira);
            _ativoRepository.Setup(x => x.GetByClienteIdAsync(carteira.ClienteId)).ReturnsAsync(AtivosFack.DadosListaAtivos());

            await carteiraAppService.AddAsync(carteiraView);
            var resultado = await _carteiraRepository.Object.GetByCarteiraIdAsync(carteira.ClienteId);

            Assert.NotNull(resultado);
            Assert.Equal(carteira.ClienteId, resultado.ClienteId);
            Assert.Equal(carteira.Ativos.FirstOrDefault()?.Quantidade, resultado.Ativos.FirstOrDefault()?.Quantidade);
            Assert.Equal(carteira.Ativos.FirstOrDefault()?.Codigo, resultado.Ativos.FirstOrDefault()?.Codigo);

            _carteiraRepository.Verify(x => x.AddAsync(It.IsAny<Carteira>()), Times.Once);

        }

        [Fact(DisplayName = "Nao adicionar dados da carteira")]
        [Trait("Carteira", "Carteiras testes")]
        public async Task AdicionarCarteira_DeveAdicionarCarteiraSemSucesso()
        {
            var carteira = CarteiraFack.DadosNaoDeveAdicionarCarteira();
            var carteiraView = CarteiraFack.DadosNaoDeveAdicionarCarteiraView();


            var carteiraAppService = new CarteiraAppServe(_carteiraRepository.Object, _ativoRepository.Object);

            _carteiraRepository.Setup(x => x.GetByCarteiraIdAsync(carteiraView.ClienteId)).ReturnsAsync(carteira);
            _ativoRepository.Setup(x => x.GetByClienteIdAsync(carteira.ClienteId)).ReturnsAsync(AtivosFack.DadosListaAtivos());

            await carteiraAppService.AddAsync(carteiraView);


            var resultado = await _carteiraRepository.Object.GetByCarteiraIdAsync(carteira.ClienteId);


            Assert.Null(resultado);
  
            _carteiraRepository.Verify(x => x.AddAsync(It.IsAny<Carteira>()), Times.Once);

        }


        [Fact (DisplayName= "Carteira tem que retornar busca a por Id")]
        [Trait("Carteira","Carteiras testes")]
        public async Task BuscarPorId_CarteirasExistente_RetornaCarteiraCorreta()
        {

            var carteira = CarteiraFack.DadosCarteira();
       
            var listaAtivos = AtivosFack.DadosListaAtivos();

            Mock<ICarteiraRepository> mockCarteira = new Mock<ICarteiraRepository>();
            Mock<IAtivoRepository> mockAtivo = new Mock<IAtivoRepository>();

            var carteiraAppService = new CarteiraAppServe(mockCarteira.Object, mockAtivo.Object);

            mockCarteira.Setup(x => x.GetByCarteiraIdAsync(carteira.ClienteId)).ReturnsAsync(carteira);
            mockAtivo.Setup(x => x.GetByClienteIdAsync(carteira.ClienteId)).ReturnsAsync(listaAtivos);

     

            var result = await mockCarteira.Object.GetByCarteiraIdAsync(carteira.ClienteId);
            var result2 = await carteiraAppService.GetByClienteIdAsync(carteira.ClienteId);

            Assert.Equal(carteira.ClienteId, result.ClienteId);
            Assert.Equal(carteira.Ativos.FirstOrDefault()?.Quantidade, result.Ativos.FirstOrDefault()?.Quantidade);
            Assert.Equal(carteira.Ativos.FirstOrDefault()?.Codigo, result.Ativos.FirstOrDefault()?.Codigo);
        }

        [Fact(DisplayName = "Carteira não tem que retornar busca a por Id")]
        [Trait("Carteira", "Carteiras testes")]
        public async Task BuscarPorId_CarteirasExistente_RetornaCarteiraIncorreta()
        {
            var carteira = CarteiraFack.DadosCarteira();

            _carteiraRepository.Setup(x => x.GetByCarteiraIdAsync(5)).ReturnsAsync(carteira);
            _ativoRepository.Setup(x => x.GetByClienteIdAsync(carteira.ClienteId)).ReturnsAsync(AtivosFack.DadosListaAtivos());

            var carteiraAppService = new CarteiraAppServe(_carteiraRepository.Object, _ativoRepository.Object);
            var resultado = await carteiraAppService.GetByClienteIdAsync(carteira.ClienteId);

            Assert.NotEqual(carteira.ClienteId, resultado.ClienteId);

        }


        [Fact(DisplayName = "Carteira tem que retornar busca todos registros")]
        [Trait("Carteira", "Carteiras testes")]
        public async Task BuscarTodas_CarteirasExistente_RetornaCarteiraCorreta()
        {
            var listaCarteiras =  CarteiraFack.DadosListaCarteira();
            _carteiraRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listaCarteiras);

            var carteiraAppService = new CarteiraAppServe(_carteiraRepository.Object, _ativoRepository.Object);
            var resultado = await carteiraAppService.GetAllAsync();

            // Assert
            Assert.NotNull(resultado);

        }


    }
}
