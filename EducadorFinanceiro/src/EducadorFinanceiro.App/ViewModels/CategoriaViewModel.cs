using EducadorFinanceiro.Business.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducadorFinanceiro.App.ViewModels
{
    public class CategoriaViewModel : EntityBaseViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Categoria")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Tipo categoria")]
        public int TipoCategoria { get; set; }

        public IEnumerable<SubCategoriaViewModel> SubCategorias { get; set; }
    }
}
