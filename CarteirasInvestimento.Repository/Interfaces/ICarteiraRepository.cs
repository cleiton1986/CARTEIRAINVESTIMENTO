using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Repository
{
    public interface ICarteiraRepository
    {
        Task<Carteira> GetByCarteiraIdAsync(int id);
        Task<IEnumerable<Carteira>> GetByCarteirasIdAsync(int id);
        Task<IEnumerable<Carteira>> GetAllAsync();
        Task AddAsync(Carteira cliente);
        Task UpdateAsync(List<Carteira> listaCarteira);
        Task<bool> ExistsRecordsByAsync();
    }
}
