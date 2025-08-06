using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(){}
        public async Task AddClienteAsync(Cliente cliente)
        {
            await AddAsync(cliente);
        }
        public async Task UpdateClienteAsync(Cliente cliente)
        {
            await UpdateAsync(cliente);
        }
        public async Task DeletarClienteAsync(Cliente cliente)
        {
            await DeleteAsync(cliente);
        }

        public async Task<IEnumerable<Cliente>> GetAllClientesAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Cliente> GetByClienteIdAsync(int id)
        {
           return await GetByIdAsync(id);
        }


    }
}
