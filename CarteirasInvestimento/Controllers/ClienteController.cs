using CarteirasInvestimento.AppServer;
using CarteirasInvestimento.AppServer.ViewModel;
using CarteirasInvestimento.Infra;
using Microsoft.AspNetCore.Mvc;

namespace CarteirasInvestimento.Controllers
{
    /// <summary>
    /// Cantroller responsável por gerenciar as operações do Cliente.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteAppServe _clienteAppServe;

        public ClienteController(IClienteAppServe clienteAppServe)
        {
            _clienteAppServe = clienteAppServe;
        }

        /// <summary>
        /// Obtém um cliente cadastrado
        /// </summary>
        /// <returns>Retorna todos clientes</returns>
        [HttpGet("getClientes")]
        public async Task<ActionResult> Get()
        {
            var clientes = await _clienteAppServe.GetAllAsync();
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());
            
            return Ok(clientes);
        }

        /// <summary>
        /// Obtém um cliente cadastrado por id.
        /// </summary>
        /// <returns>Retorna cliente por Id</returns>
        /// <param name="id">Id, paramentro de busca do cliente</param>

        [HttpGet("getCliente/{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var clientes = await _clienteAppServe.GetByClienteIdAsync(id);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());
            
            return Ok(clientes);
        }

        /// <summary>
        /// Cadastra um cliente.
        /// </summary>
        /// <param name="clienteView"></param>
        /// <returns>Cliente cadastrado com sucesso!</returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Cliente cadastrado com sucesso!</response>
        /// <param name="clienteView">Dados do cliente a ser cadastro</param>
        [ProducesResponseType(typeof(ProblemDetails),StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async  Task<IActionResult> Post([FromBody] ClienteCadastroView clienteView)
        {
            await _clienteAppServe.AddAsync(clienteView);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(new { mensagem = "Cliente cadastrado com sucesso!" });
        }

        /// <summary>
        /// Atualizar um cliente.
        /// </summary>
        /// <param name="clienteView"></param>
        /// <returns>Cliente atualizado com sucesso!</returns>
        /// <response code="400">Retorna erros de validação</response>
        /// <response code="200">Cliente atualizado com sucesso!</response>
        /// <param name="clienteView">Dados do cliente a ser atualizado</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClienteEditarView clienteView)
        {
            await _clienteAppServe.UpdateAsync(clienteView);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());
            
            return Ok(new { mensagem = "Cliente atualizado com sucesso!" });
        }

        /// <summary>
        /// Obtém um cliente cadastrado
        /// </summary>
        /// <returns>Retorna todos clientes</returns>
        /// <response code="200">Cliente deletado com sucesso!</response>
        /// <response code="400">Erro ao deletar o cliente</response>
        /// <param name="id">Id, paramentro de busca para deletar cliente</param>
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _clienteAppServe.DeletarAsync(id);
            if (!Notification.IsValid())
                return BadRequest(Notification.GetErrors());

            return Ok(new {mensagem = "Cliente cadastrado com sucesso!"});
        }

    }
}
