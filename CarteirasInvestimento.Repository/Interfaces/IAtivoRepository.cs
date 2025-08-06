using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Repository.Interfaces
{
    public interface IAtivoRepository
    {
        Task<IEnumerable<Ativo>> GetAllAsync();
        Task<Ativo> GetByClienteIdAsync(int id);
        Task AddAtivoAsync(Ativo ativo);
        Task UpdateAtivoAsync(Ativo ativo);
        Task DeleteAtivoAsync(Ativo ativo);
        Task<Ativo> GetByIdAsync(int id);
    }
}

