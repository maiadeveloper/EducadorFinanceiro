using System;

namespace EducadorFinanceiro.Business.Models
{
    public class Lancamento : EntityBase
    {
        public Guid FavorecidoId { get; set; }

        public Guid SubCategoriaId { get; set; }

        public DateTime DataVencimento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public bool Status { get; set; }

        /*Entity Framework Relacionamento */
        public Favorecido Favorecido { get; set; }

        /*Entity Framework Relacionamento */
        public SubCategoria SubCategoria { get; set; }
    }
}
