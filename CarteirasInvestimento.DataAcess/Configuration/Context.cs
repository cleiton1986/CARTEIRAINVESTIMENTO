using CarteirasInvestimento.DataAcess.Configuration.Map;
using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.EntityFrameworkCore;

namespace CarteirasInvestimento.DataAcess.Configuration
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Carteira> Carteiras { get; set; }
        public DbSet<Ativo> Ativos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CarteiraConfiguration());
            modelBuilder.ApplyConfiguration(new AtivoConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                 optionsBuilder.UseSqlite("Data Source=CarteiraInvestimento.db");
                //optionsBuilder.UseSqlServer("Data Source=CLEITON\\SQLEXPRESS;Initial Catalog=CarteiraInvestimento;Integrated Security=True;Encrypt=False"); 
            }
        }
    }
}
