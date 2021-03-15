using EducadorFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Text;

namespace EducadorFinanceiro.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.TipoCategoria)
                .IsRequired()
                .HasColumnType("int");

            // 1 : N => Categoria : SubCategorias
            builder.HasMany(c => c.SubCategorias)
                .WithOne(s => s.Categoria)
                .HasForeignKey(s => s.CategoriaId);

            builder.ToTable("Categorias");
        }
    }
}
