using System;
using System.Collections.Generic;

namespace EducadorFinanceiro.Business.Models
{
    public class SubCategoria : EntityBase
    {
        public Guid CategoriaId { get; set; }

        public string Nome { get; set; }

        /*Entity Framework Relacionamento */
        public Categoria Categoria { get; set; }

        /*Entity Framework Relacionamento*/
        public IEnumerable<Lancamento> Lancamentos { get; set; }
    }
}
