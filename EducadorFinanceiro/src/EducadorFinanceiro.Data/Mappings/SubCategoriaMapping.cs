using EducadorFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EducadorFinanceiro.Data.Mappings
{
    public class SubCategoriaMapping : IEntityTypeConfiguration<SubCategoria>
    {
        public void Configure(EntityTypeBuilder<SubCategoria> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            // 1 : N => SubCategoria : Lacamentos
            builder.HasMany(s => s.Lancamentos)
                .WithOne(l => l.SubCategoria)
                .HasForeignKey(l => l.SubCategoriaId);

            builder.ToTable("SubCategorias");
        }
    }
}
