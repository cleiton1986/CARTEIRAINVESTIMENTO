using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.AspNetCore.Mvc;
namespace CarteirasInvestimento.Controllers
{
    /// <summary>
    ///   Controller responsável por gerenciar as operações do  Investimento para a entidade Carteira.  
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class AtivosController : ControllerBase
    {
        private readonly IAtivoAppServe _ativoAppServe;

        public AtivosController(IAtivoAppServe ativoAppServe)
        {
            _ativoAppServe = ativoAppServe;
        }

        /// <summary>
        /// Obtém uma lista de todas as carteiras cadastradas.
        /// </summary>  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carteira>>> GetAll()
        {
            var cliente = await _ativoAppServe.GetAllAsync();

            return Ok(cliente);
        }


    }
}
