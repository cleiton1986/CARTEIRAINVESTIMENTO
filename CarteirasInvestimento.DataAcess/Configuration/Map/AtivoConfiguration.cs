using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteirasInvestimento.DataAcess.Configuration
{
    public class AtivoConfiguration: IEntityTypeConfiguration<Ativo>
    {
        public void Configure(EntityTypeBuilder<Ativo> builder)
        {
            builder.ToTable(nameof(Ativo));
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Codigo)
                    .HasColumnName("Codigo")
                    .HasColumnType("VARCHAR(20)")
                    .IsRequired()
                    .HasMaxLength(20);

            builder.Property(a => a.Nome)
                   .HasColumnName("Nome")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(a => a.Quantidade)
                   .HasColumnName("Quantidade")
                   .HasColumnType("INT")
                   .IsRequired();

            builder.Property(a => a.Tipo)
                   .HasColumnName("Tipo")
                   .HasColumnType("INT")
                   .IsRequired();

            builder.Property(a => a.PrecoUnitario)
                   .HasColumnName("PrecoUnitario")
                   .HasColumnType("DECIMAL")
                   .IsRequired();

            builder.HasOne(c => c.Carteira)
                   .WithMany(c => c.Ativos)
                   .HasForeignKey(a => a.CarteiraId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
