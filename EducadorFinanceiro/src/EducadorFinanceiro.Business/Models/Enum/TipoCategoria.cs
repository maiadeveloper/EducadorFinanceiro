using System.ComponentModel.DataAnnotations;

namespace EducadorFinanceiro.Business.Models.Enum
{
    public enum TipoCategoria
    {
        [Display(Name = "Receita Fixa")]
        ReceitaFixa = 1,

        [Display(Name = "Receita Variável")]
        ReceitaVariavel = 2,

        [Display(Name = "Despesa Fixa")]
        DespesaFixa = 3,

        [Display(Name = "Despesa Variável")]
        DespesaVariavel = 4,
    }
}
