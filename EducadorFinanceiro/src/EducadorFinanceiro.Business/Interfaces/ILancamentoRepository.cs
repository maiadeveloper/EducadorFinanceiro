using EducadorFinanceiro.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EducadorFinanceiro.Business.Interfaces
{
    public interface ILancamentoRepository : IRepository<Lancamento>
    {
        Task<IEnumerable<Lancamento>> ObterTodosLancamentos();

        Task<IEnumerable<Lancamento>> ObterTodosLancamentosPorPeriodoDespesas(DateTime dataInicio, DateTime dataFim);

        Task<IEnumerable<Lancamento>> ObterTodosLancamentosPorPeriodoReceitas(DateTime dataInicio, DateTime dataFim);
    }
}
