using EducadorFinanceiro.Business.Interfaces;
using EducadorFinanceiro.Business.Models;
using EducadorFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducadorFinanceiro.Data.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MeuDbContext meuDbContext) : base(meuDbContext) { }

        public async Task<Categoria> ObterCategoria(Guid id)
        {
            return await _meuDbContext.Categorias.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _meuDbContext.Categorias.AsNoTracking().OrderBy(c => c.Nome).ToListAsync();
        }
    }
}
