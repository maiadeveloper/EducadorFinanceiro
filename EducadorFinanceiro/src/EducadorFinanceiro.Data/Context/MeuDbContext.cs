using EducadorFinanceiro.Business.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EducadorFinanceiro.Data.Context
{
    public class MeuDbContext : DbContext
    {
        public MeuDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Lancamento> Lancamentos { get; set; }

        public DbSet<Favorecido> Favorecidos { get; set; }

        public DbSet<Categoria> Categorias { get; set; }

        public DbSet<SubCategoria> SubCategorias { get; set; }

        //Sobreescreve alguma configuração desejada
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

            //Desabilita o efeito de delete e cascata
            foreach (var relationship in modelBuilder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
