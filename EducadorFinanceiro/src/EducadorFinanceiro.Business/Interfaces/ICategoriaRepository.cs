using EducadorFinanceiro.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducadorFinanceiro.Business.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterCategorias();

        Task<Categoria> ObterCategoria(Guid id);
    }
}
