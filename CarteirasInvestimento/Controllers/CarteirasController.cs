using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.Infra;
using Microsoft.AspNetCore.Mvc;

namespace CarteirasInvestimento.Controllers
{
    /// <summary>
    ///   Controller responsável por gerenciar as operações da Carteira de Investimento para a entidade Carteira.  
    /// </summary>


    [Route("api/[controller]")]
    [ApiController]
    public class CarteirasController :  ControllerBase
    {
        private readonly ICarteiraAppServe _carteiraAppServe;

        public CarteirasController(ICarteiraAppServe carteiraAppServe)
        {
            _carteiraAppServe = carteiraAppServe;
        }

        /// <summary>
        /// Obtém uma carteira cadastradas por id do cliente.
        /// </summary>

        [HttpGet("{clienteId}")]
        public async Task<ActionResult> Get(int clienteId)
        {
            var cliente = await _carteiraAppServe.GetByClienteIdAsync(clienteId);

            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());
 
            return Ok(cliente);
        }

        /// <summary>
        /// Obtém uma lista de todas as carteiras cadastradas.
        /// </summary>     
        [HttpGet()]
        public async Task<ActionResult> GetAll()
        {
            var cliente = await _carteiraAppServe.GetAllAsync();

            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(cliente);
        }
        /// <summary>
        /// Cadastra uma carteiras.
        /// </summary> 
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CarteiraCadastroView view)
        {
       
            await _carteiraAppServe.AddAsync(view);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(new { mensagem = "Carteira criada com sucesso!" });

        }
    }
}
