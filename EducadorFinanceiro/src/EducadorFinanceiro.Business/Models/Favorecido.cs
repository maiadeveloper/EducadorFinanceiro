using EducadorFinanceiro.Business.Models.Enum;
using System.Collections.Generic;

namespace EducadorFinanceiro.Business.Models
{
    public class Favorecido : EntityBase
    {
        public TipoFavorecido TipoFavorecido { get; set; }

            public string NomeFantasia { get; set; }

            public string RazaoSocial { get; set; }

            public string Documento { get; set; }

            /*Entity Framework Relacionamento*/
            public IEnumerable<Lancamento> Lancamentos { get; set; }
    }
}
