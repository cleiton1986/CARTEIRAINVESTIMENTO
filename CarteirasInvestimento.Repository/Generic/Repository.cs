using CarteirasInvestimento.DataAcess.Configuration;
using Microsoft.EntityFrameworkCore;

namespace CarteirasInvestimento.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContextOptions<Context> _context;
        public Repository()
        {
            _context = new DbContextOptions<Context>();
        }

        public async Task AddAsync(T entity)
        {

            using (var data = new Context(_context))
            {
                data.Add(entity);
                await data.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(T entity)
        {
            using (var data = new Context(_context))
            {
                data.Remove(entity);
                await data.SaveChangesAsync();
            }
        }
        public async Task UpdateAsync(T entity)
        {
            using (var data = new Context(_context))
            {
                data.Update(entity);
                await data.SaveChangesAsync();
            }
        }
        public async Task<IList<T>> GetAllAsync()
        {
            using (var data = new Context(_context))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            using (var data = new Context(_context))
            {
                return await data.Set<T>().FindAsync(id);
            }
        }

     
    }
}
