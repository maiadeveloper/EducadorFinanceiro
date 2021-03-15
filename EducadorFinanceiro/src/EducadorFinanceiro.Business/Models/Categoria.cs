using EducadorFinanceiro.Business.Models.Enum;
using System.Collections.Generic;

namespace EducadorFinanceiro.Business.Models
{
    public class Categoria : EntityBase
    {
        public string Nome { get; set; }

        public TipoCategoria TipoCategoria { get; set; }

        /*Entity Framework Relacionamento */
        public IEnumerable<SubCategoria> SubCategorias { get; set; }
    }
}
