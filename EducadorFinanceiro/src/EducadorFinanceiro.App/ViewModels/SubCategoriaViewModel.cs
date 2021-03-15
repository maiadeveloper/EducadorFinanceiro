using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducadorFinanceiro.App.ViewModels
{
    public class SubCategoriaViewModel : EntityBaseViewModel
    {
        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [DisplayName("Subcategoria")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="O campo {0} é obrigatório")]
        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }

        public CategoriaViewModel Categoria { get; set; }

        public IEnumerable<LancamentoViewModel> Lancamentos { get; set; }

        public IEnumerable<CategoriaViewModel> Categorias { get; set; }
    }
}
