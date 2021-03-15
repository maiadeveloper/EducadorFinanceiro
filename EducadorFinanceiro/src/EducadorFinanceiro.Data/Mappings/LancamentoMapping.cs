using EducadorFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducadorFinanceiro.Data.Mappings
{
    public class LancamentoMapping : IEntityTypeConfiguration<Lancamento>
    {
        public void Configure(EntityTypeBuilder<Lancamento> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Descricao)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.ToTable("Lancamentos");
        }
    }
}
