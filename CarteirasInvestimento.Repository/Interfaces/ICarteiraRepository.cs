using CarteirasInvestimento.DataAcess.Entity;

namespace CarteirasInvestimento.Repository
{
    public interface ICarteiraRepository
    {
        Task<Carteira> GetByCarteiraIdAsync(int id);
        Task<Carteira> GetByCarteirasIdAsync(int id);
        Task<IEnumerable<Carteira>> GetAllAsync();
        Task AddAsync(Carteira carteira);
        Task UpdateAsync(Carteira carteira);
        Task DeletarAsync(Carteira carteira);
        Task<bool> ExistsRecordsByAsync();
    }
}
