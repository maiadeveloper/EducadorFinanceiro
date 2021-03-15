using Microsoft.AspNetCore.Mvc.Razor;
using System;

namespace EducadorFinanceiro.App.Extensions
{
    public static class RazorExtensions
    {
        public static string FormatarDocumento(this RazorPage page, int tipoFavorecido, string documento)
        {
            return tipoFavorecido == 1 ? Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00");
        }

        public static string ExibiSituacaoAtivoOuDesativo(this RazorPage page, bool situacao)
        {
            return situacao == true ? "Ativo" : "Desativado";
        }

        public static string FormatarData(this RazorPage page, string data)
        {
            return Convert.ToDateTime(data).ToString("dd/MM/yyyy");
        }

        public static string FormatarMoeda(this RazorPage page, decimal valor)
        {
            return valor.ToString("C");
        }

        public static string FormatarDescricaoTrim(this RazorPage page, string descricao)
        {
            return descricao.Length <= 25 ? descricao : descricao = descricao.Substring(0, 25) + "...";
        }
    }
}
