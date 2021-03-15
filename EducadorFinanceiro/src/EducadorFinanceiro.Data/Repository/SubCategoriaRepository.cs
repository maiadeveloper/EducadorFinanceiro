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
    public class SubCategoriaRepository : Repository<SubCategoria>, ISubCategoriaRepository
    {
        public SubCategoriaRepository(MeuDbContext meuDbContext) : base(meuDbContext) { }

        public async Task<SubCategoria> ObterSubcategoria(Guid id)
        {
            return await _meuDbContext.SubCategorias
                            .AsNoTracking()
                                .Include(s => s.Categoria)
                                    .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<SubCategoria>> ObterSubcategorias()
        {
            return await _meuDbContext.SubCategorias
                            .AsNoTracking()
                                .Include(s => s.Categoria)
                                    .OrderBy(s => s.Nome)
                                        .ToListAsync();
        }

        public async Task<IEnumerable<SubCategoria>> ObterSubcategoriasPorCategoriaId(Guid categoriaId)
        {
            return await Buscar(s => s.CategoriaId == categoriaId);
        }
    }
}
