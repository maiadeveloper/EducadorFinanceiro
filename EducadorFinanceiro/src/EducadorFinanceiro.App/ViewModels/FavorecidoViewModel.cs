using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EducadorFinanceiro.App.ViewModels
{
    public class FavorecidoViewModel : EntityBaseViewModel
    {
        [Required(ErrorMessage ="O campo {0} é obrigtório")]
        [DisplayName("Tipo de pessoa")]
        public int TipoFavorecido { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Documento")]
        public string Documento { get; set; }

        public IEnumerable<LancamentoViewModel> Lancamentos { get; set; }
    }
}
