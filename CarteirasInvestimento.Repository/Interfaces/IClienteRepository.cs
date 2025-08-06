using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Repository
{
    public interface IClienteRepository
    {
        Task DeletarClienteAsync(Cliente cliente);
        Task UpdateClienteAsync(Cliente cliente);
        Task AddClienteAsync(Cliente cliente);
        Task<IEnumerable<Cliente>> GetAllClientesAsync();
        Task<Cliente> GetByClienteIdAsync(int id);

    }
}
