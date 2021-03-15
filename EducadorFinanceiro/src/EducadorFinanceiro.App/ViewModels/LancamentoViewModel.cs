using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EducadorFinanceiro.App.ViewModels
{
    public class LancamentoViewModel : EntityBaseViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Favorecido")]
        public Guid FavorecidoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Categoria")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Subcategoria")]
        public Guid SubCategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Data Vencimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime DataVencimento { get; set; }

        [DisplayName("Data Pagamento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? DataPagamento { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigtório")]

        [DisplayName("Valor")]
        public decimal Valor { get; set; }

        [DisplayName("Pago?")]
        public bool Status { get; set; }

        [DisplayName("Quantidade Parcela")]
        [Required(ErrorMessage = "O campo {0} é obrigtório")]
        public int QtdeParcela { get; set; }

        public FavorecidoViewModel Favorecido { get; set; }

        public SubCategoriaViewModel SubCategoria { get; set; }

        public IEnumerable<FavorecidoViewModel> Favorecidos { get; set; }

        public IEnumerable<SubCategoriaViewModel> SubCategorias { get; set; }
    }
}
