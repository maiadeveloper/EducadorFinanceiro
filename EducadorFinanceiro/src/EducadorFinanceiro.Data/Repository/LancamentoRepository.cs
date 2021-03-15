using EducadorFinanceiro.Business.Interfaces;
using EducadorFinanceiro.Business.Models;
using EducadorFinanceiro.Business.Models.Enum;
using EducadorFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducadorFinanceiro.Data.Repository
{
    public class LancamentoRepository : Repository<Lancamento>, ILancamentoRepository
    {
        public LancamentoRepository(MeuDbContext meuDbContext) : base(meuDbContext) { }

        public async Task<IEnumerable<Lancamento>> ObterTodosLancamentos()
        {
            return await _meuDbContext.Lancamentos
                                            .AsNoTracking()
                                                .Include(l => l.Favorecido)
                                                    .Include(l => l.SubCategoria)
                                                        .Include(l => l.SubCategoria.Categoria)
                                                            .OrderBy(l => l.DataCadastro)
                                                                .ToListAsync();
        }

        public async Task<IEnumerable<Lancamento>> ObterTodosLancamentosPorPeriodoDespesas(DateTime dataInicio, DateTime dataFim)
        {
            return await _meuDbContext.Lancamentos
                                    .AsNoTracking()
                                         .Include(l => l.Favorecido)
                                            .Include(l => l.SubCategoria)
                                                .Include(l => l.SubCategoria.Categoria)
                                                    .Where(l => l.DataVencimento >= dataInicio && l.DataVencimento <= dataFim &&
                                                            l.SubCategoria.Categoria.TipoCategoria == TipoCategoria.DespesaFixa || 
                                                                l.SubCategoria.Categoria.TipoCategoria == TipoCategoria.DespesaVariavel)
                                                                 .ToListAsync();
        }

        public async Task<IEnumerable<Lancamento>> ObterTodosLancamentosPorPeriodoReceitas(DateTime dataInicio, DateTime dataFim)
        {
            return await _meuDbContext.Lancamentos
                                    .AsNoTracking()
                                         .Include(l => l.Favorecido)
                                            .Include(l => l.SubCategoria)
                                                .Include(l => l.SubCategoria.Categoria)
                                                    .Where(l => l.DataVencimento >= dataInicio && l.DataVencimento <= dataFim &&
                                                            l.SubCategoria.Categoria.TipoCategoria == TipoCategoria.ReceitaFixa || 
                                                                l.SubCategoria.Categoria.TipoCategoria == TipoCategoria.ReceitaVariavel)
                                                                    .ToListAsync();
        }
    }
}
