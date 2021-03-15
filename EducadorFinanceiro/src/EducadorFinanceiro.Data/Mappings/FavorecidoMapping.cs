using EducadorFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducadorFinanceiro.Data.Mappings
{
    public class FavorecidoMapping : IEntityTypeConfiguration<Favorecido>
    {
        public void Configure(EntityTypeBuilder<Favorecido> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.NomeFantasia)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(f => f.RazaoSocial)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(f => f.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // 1 : N => Favorecido : Lancamentos
            builder.HasMany(f => f.Lancamentos)
                .WithOne(l => l.Favorecido)
                .HasForeignKey(l => l.FavorecidoId);

            builder.ToTable("Favorecidos");
        }
    }
}
