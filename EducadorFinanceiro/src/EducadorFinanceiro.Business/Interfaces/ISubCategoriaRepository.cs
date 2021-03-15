using EducadorFinanceiro.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducadorFinanceiro.Business.Interfaces
{
    public interface ISubCategoriaRepository : IRepository<SubCategoria>
    {
        Task<IEnumerable<SubCategoria>> ObterSubcategoriasPorCategoriaId(Guid categoriaId);

        Task<IEnumerable<SubCategoria>> ObterSubcategorias();

        Task<SubCategoria> ObterSubcategoria(Guid id);
    }
}
