using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarteirasInvestimento.DataAcess.Configuration
{
    public class Context : DbContext
    {
        public DbSet<Carteira> Clientes { get; set; }
        public DbSet<Ativo> Ativos { get; set; }
        public Context(DbContextOptions<Context> options) : base(options){}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CarteiraConfiguration());
            modelBuilder.ApplyConfiguration(new AtivoConfiguration());
        }
    }
}
