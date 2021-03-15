using System.ComponentModel.DataAnnotations;

namespace EducadorFinanceiro.Business.Models.Enum
{
    public enum TipoFavorecido
    {
        [Display(Name ="Pessoa Física")]
        PessoaFisica = 1,

        [Display(Name ="Pessoa Jurídica")]
        PessoaJuridica = 2
    }
}
