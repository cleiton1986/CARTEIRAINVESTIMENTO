using AutoMapper;
using CarteirasInvestimento.AppServer.ViewModel;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Infra;
using CarteirasInvestimento.Repository;

namespace CarteirasInvestimento.AppServer
{
    public class ClienteAppServe: IClienteAppServe
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public ClienteAppServe(
                        IClienteRepository clienteRepository,
                        IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task DeletarAsync(int id)
        {
            try
            {
                if (Extensions.ValidateInt(id, "Id do cliente é obrigatório."))
                    return;
                
                var cliente = await _clienteRepository.GetByClienteIdAsync(id);
                await _clienteRepository.DeletarClienteAsync(cliente);
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao deletar cliente. : {ex.Message} ", "DeletarAsync");
            }
        }

        public async Task<IEnumerable<ClienteView>> GetAllAsync()
        {
            var cliente = await _clienteRepository.GetAllClientesAsync();
            var listClienteView = _mapper.Map<List<ClienteView>>(cliente);

            return listClienteView;
        }

        public async Task<ClienteView> GetByClienteIdAsync(int id)
        {
            var clienteView = new ClienteView();
            try
            {
                if (Extensions.ValidateInt(id, "Id do cliente é obrigatório."))
                    return clienteView;

                var cliente = await _clienteRepository.GetByClienteIdAsync(id);
                clienteView = _mapper.Map<ClienteView>(cliente);
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao buscar cliente por id. : {ex.Message} ", "GetByClienteIdAsync");
            }

            return clienteView;
        }
        public async Task UpdateAsync(ClienteEditarView clienteView)
        {
            try
            {
                var cliente = await _clienteRepository.GetByClienteIdAsync(clienteView.Id);

                if (cliente == null)
                {
                    Notification.Notify($"Cliente com Id: {clienteView.Id} não encontrado.", "UpdateAsync");
                    return;
                }

                cliente.Nome = clienteView.Nome;
                cliente.Cep = clienteView.Cep;
                cliente.Telefone = clienteView.Telefone;
                cliente.Email = clienteView.Email;
                cliente.Complemento = clienteView.Complemento;
                cliente.Bairro = clienteView.Cep;
                cliente.DataAtualizacao = DateTime.Now;
                cliente.DataNascimento = clienteView.DataNascimento.ConvertStringToDate();

                await _clienteRepository.UpdateClienteAsync(cliente);
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao atualizar cliente. : {ex.Message} ", "UpdateAsync");
            }

        }

        public async Task AddAsync(ClienteCadastroView clienteView)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteView);
                if (cliente != null && cliente.Validate())
                {
                    await _clienteRepository.AddClienteAsync(cliente);
                }
            }
            catch (Exception ex)
            {
                Notification.Notify($"Erro ao cadastar cliente. : {ex.Message} ", "AddAsync");
            }
            
        }

    }
}
