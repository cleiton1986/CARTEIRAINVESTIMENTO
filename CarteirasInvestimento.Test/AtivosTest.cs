using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.Repository.Interfaces;
using Moq;

namespace CarteirasInvestimento.Test
{
    public class AtivosTest
    {
        public readonly Mock<IAtivoRepository> _ativoRepository;
        public AtivosTest()
        {
            _ativoRepository = new Mock<IAtivoRepository>();
        }

        [Fact(DisplayName = "Buscar todos os ativos")]
        [Trait("Ativos", "Ativos testes")]
        public async Task BuscarTodos_AtivosExistente_RetornaCarteiraCorreta()
        {
            var listaAtivos = AtivosFack.DadosListaAtivos();
            _ativoRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(listaAtivos);

            var ativoAppService = new AtivoAppServe(_ativoRepository.Object);
            var resultado = await ativoAppService.GetAllAsync();

            Assert.NotNull(resultado);
        }

        [Fact(DisplayName = "Ativo tem que retornar busca a por Id")]
        [Trait("Ativos", "Ativos testes")]
        public async Task BuscarPorId_AtivosExistente_RetornaAtivoCorreto()
        {
            var listaAtivos = AtivosFack.DadosListaAtivos();

            _ativoRepository.Setup(x => x.GetByClienteIdAsync(1)).ReturnsAsync(listaAtivos);
            var resultado = await _ativoRepository.Object.GetByClienteIdAsync(1);

            Assert.NotNull(resultado);
            Assert.Equal(listaAtivos.FirstOrDefault()?.PrecoUnitario, resultado.FirstOrDefault()?.PrecoUnitario);
            Assert.Equal(listaAtivos.FirstOrDefault()?.Nome, resultado.FirstOrDefault()?.Nome);
            Assert.Equal(listaAtivos.FirstOrDefault()?.CarteiraId, resultado.FirstOrDefault()?.CarteiraId);
            Assert.Equal(listaAtivos.FirstOrDefault()?.Codigo, resultado.FirstOrDefault()?.Codigo);
            Assert.Equal(listaAtivos.FirstOrDefault()?.Quantidade, resultado.FirstOrDefault()?.Quantidade);
            Assert.Equal(listaAtivos.FirstOrDefault()?.Tipo, resultado.FirstOrDefault()?.Tipo);
        }

    }
}
