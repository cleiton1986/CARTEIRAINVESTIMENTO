using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.AppServer.Interfaces;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Infra;
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
        /// Obtém um ativo cadastrado
        /// </summary>
        /// <returns>Retorna todos Ativos</returns>
        [HttpGet("getAtivos")]
        public async Task<ActionResult> Get()
        {
            var clientes = await _ativoAppServe.GetAtivosAllAsync();
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(clientes);
        }

        /// <summary>
        /// Obtém um ativo cadastrado por id.
        /// </summary>
        /// <returns>Retorna ativo por Id</returns>
        /// <param name="id">Id, paramentro de busca do ativo</param>

        [HttpGet("getAtivo/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var clientes = await _ativoAppServe.GetByIdAsync(id);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(clientes);
        }

        /// <summary>
        /// Cadastra um ativo.
        /// </summary>
        /// <param name="ativoView"></param>
        /// <returns>Cliente cadastrado com sucesso!</returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Cliente cadastrado com sucesso!</response>
        /// <param name="ativoView">Dados do cliente a ser cadastro</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AtivoView ativoView)
        {
            await _ativoAppServe.AddAtivoAsync(ativoView);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(new { mensagem = "Ativo cadastrado com sucesso!" });
        }


        /// <summary>
        /// Atualizar um ativo.
        /// </summary>
        /// <param name="ativoView"></param>
        /// <returns>Ativo atualizado com sucesso!</returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Ativo atualizado com sucesso!</response>
        /// <param name="ativoView">Dados do cliente a ser atualizado</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] AtivoView ativoView)
        {
            await _ativoAppServe.UpdateAtivoAsync(ativoView);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(new { mensagem = "Ativo atualizado com sucesso!" });
        }

        /// <summary>
        /// Deleta um ativo cadastrado
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Ativo deletado com sucesso!</response>
        /// <response code="400">Erro ao deletar o ativo</response>
        /// <param name="id">Id, paramentro de busca para deletar ativo</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _ativoAppServe.DeleteAtivoAsync(id);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(new { mensagem = "Ativo cadastrado com sucesso!" });
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
