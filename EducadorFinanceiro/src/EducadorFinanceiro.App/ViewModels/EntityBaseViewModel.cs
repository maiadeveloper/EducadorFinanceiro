using System;
using System.ComponentModel.DataAnnotations;

namespace EducadorFinanceiro.App.ViewModels
{
    public abstract class EntityBaseViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [ScaffoldColumn(false)]
        public DateTime DataCadastro { get; set; }

        [Display(Name ="Situação")]
        public bool Ativo { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? DataExclusao { get; set; }
    }
}
