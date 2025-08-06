using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteirasInvestimento.DataAcess.Configuration
{
    public class CarteiraConfiguration : IEntityTypeConfiguration<Carteira>
    {
        public void Configure(EntityTypeBuilder<Carteira> builder)
        {

            builder.ToTable(nameof(Carteira));
            builder.HasKey(c => c.Id);

            builder.Property(c => c.ClienteId)
                   .HasColumnName("ClienteId")
                   .HasColumnType("INT")
                   .IsRequired();

            builder.Property(c => c.AtivoId)
                    .HasColumnName("AtivoId")
                    .HasColumnType("INT")
                    .IsRequired();

            builder.Property(c => c.DataCadastro)
               .HasColumnName("DataCadastro")
               .HasColumnType("DATETIME")
               .IsRequired();


           builder.HasOne(c => c.Cliente)
                   .WithMany(c => c.Carteiras)
                   .HasForeignKey(c => c.ClienteId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
