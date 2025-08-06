using CarteirasInvestimento.DataAcess.Configuration;
using CarteirasInvestimento.DataAcess.Entity;
using CarteirasInvestimento.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CarteirasInvestimento.Repository
{
    public class AtivoRepository : IAtivoRepository
    {
        private readonly Context _context;
        public AtivoRepository(Context context) {
            _context = context;
        }
        public async Task<IEnumerable<Ativo>> GetAllAsync()
        {
            return await _context.Ativos.Include(x => x.Carteiras)
                .ThenInclude(x => x.Cliente)
                .ToListAsync();
        }

        public async Task AddAtivoAsync(Ativo ativo)
        {
            await _context.Ativos.AddAsync(ativo);
            await _context.SaveChangesAsync();

        }
        public async Task UpdateAtivoAsync(Ativo ativo)
        {
             _context.Ativos.Update(ativo);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAtivoAsync(Ativo ativo)
        {
             _context.Ativos.Remove(ativo);
            await _context.SaveChangesAsync();  
        }
        public async Task<Ativo> GetByClienteIdAsync(int id)
        {
            return  await _context.Ativos.FindAsync(id);
        }
        public async Task<Ativo> GetByIdAsync(int id)
        {
            return await _context.Ativos.FindAsync(id);
        }
    }
}
