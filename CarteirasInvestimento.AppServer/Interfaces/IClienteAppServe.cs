using CarteirasInvestimento.AppServer.ViewModel;

namespace CarteirasInvestimento.AppServer
{
    public interface IClienteAppServe
    {
        Task DeletarAsync(int id);
        Task UpdateAsync(ClienteEditarView clienteView);
        Task AddAsync(ClienteCadastroView clienteView);
        Task<IEnumerable<ClienteView>> GetAllAsync();
        Task<ClienteView> GetByClienteIdAsync(int id);
    }
}
