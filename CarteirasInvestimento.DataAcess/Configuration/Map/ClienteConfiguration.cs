using CarteirasInvestimento.DataAcess.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarteirasInvestimento.DataAcess.Configuration.Map
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable(nameof(Cliente));
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                   .HasColumnName("Nome")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Email)
                   .HasColumnName("Email")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Telefone)
                   .HasColumnName("Telefone")
                   .HasColumnType("VARCHAR(15)")
                   .IsRequired()
                   .HasMaxLength(15);

            builder.Property(c => c.Logadouro)
                   .HasColumnName("Logadouro")
                   .HasColumnType("VARCHAR(100)")
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.Numero)
                  .HasColumnName("Numero")
                  .HasColumnType("INT")
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(c => c.Cep)
                  .HasColumnName("Cep")
                  .HasColumnType("VARCHAR(10)")
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(c => c.Bairro)
                  .HasColumnName("Bairro")
                  .HasColumnType("VARCHAR(100)")
                  .IsRequired()
                  .HasMaxLength(100);


            builder.Property(c => c.Complemento)
                  .HasColumnName("Complemento")
                  .HasColumnType("VARCHAR(100)")
                  .IsRequired(false);

            builder.Property(c => c.DataCadastro)
                   .HasColumnName("DataCadastro")
                   .HasColumnType("DATETIME")
                   .IsRequired();

            builder.Property(c => c.DataNascimento)
                .HasColumnName("DataNascimento")
                .HasColumnType("DATETIME")
                .IsRequired();

            builder.Property(c => c.DataAtualizacao)
                 .HasColumnName("DataAtualizacao")
                 .HasColumnType("DATETIME")
                 .IsRequired(false);
        }
    }
}
