using CarteirasInvestimento.DataAcess.Configuration;
using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarteirasInvestimento.Repository
{
    public class CarteiraRepository : Repository<Carteira>, ICarteiraRepository
    {
        private readonly Context _context;
        public CarteiraRepository(Context context)
        {
            _context = context;
        }

        public async Task AddAsync(Carteira carteira)
        {
            await AddAsync(carteira);
        }

        public async Task UpdateAsync(Carteira carteira)
        {
            await UpdateAsync(carteira);
        }
        public async Task DeletarAsync(Carteira carteira)
        {
            await DeletarAsync(carteira);
        }
        public async Task<Carteira> GetByCarteiraIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }
        public async Task<Carteira> GetByCarteirasIdAsync(int id)
        {
            return await _context.Carteiras.Include(c => c.Ativos)
                                           .FirstOrDefaultAsync(c => c.ClienteId == id);
        }
        public async Task<IEnumerable<Carteira>> GetAllAsync()
        {
            return await GetAllAsync();
        }

        public async Task<bool> ExistsRecordsByAsync()
        {
            return await _context.Carteiras.AnyAsync();
        }

     
    }
}
