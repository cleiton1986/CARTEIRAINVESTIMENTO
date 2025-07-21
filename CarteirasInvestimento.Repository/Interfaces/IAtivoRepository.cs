using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Repository.Interfaces
{
    public interface IAtivoRepository
    {
        Task<IEnumerable<Ativo>> GetAllAsync();
        Task<IEnumerable<Ativo>> GetByClienteIdAsync(int id);
        Task AddAsync(List<Ativo> listaAtivo);

    }
}
