using AutoMapper;
using EducadorFinanceiro.App.Extensions;
using EducadorFinanceiro.App.ViewModels;
using EducadorFinanceiro.Business.Interfaces;
using EducadorFinanceiro.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EducadorFinanceiro.App.Controllers
{

    [Authorize]
    public class LancamentoController : Controller
    {
        private readonly ILancamentoRepository _lancamentoRepository;
        private readonly ISubCategoriaRepository _subCategoriaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IFavorecidoRepository _favorecidoRepository;
        private readonly IMapper _mapper;

        public LancamentoController(ILancamentoRepository lancamentoRepository,
                                    ISubCategoriaRepository subCategoriaRepository,
                                    ICategoriaRepository categoriaRepository,
                                    IFavorecidoRepository favorecidoRepository,
                                    IMapper mapper)
        {
            _lancamentoRepository = lancamentoRepository;
            _subCategoriaRepository = subCategoriaRepository;
            _categoriaRepository = categoriaRepository;
            _favorecidoRepository = favorecidoRepository;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("meus-lancamentos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<LancamentoViewModel>>(await _lancamentoRepository.ObterTodosLancamentos()));
        }

        [Route("adicionar-lancamento")]
        [ClaimsAuthorize("Lancamento", "Adicionar")]
        public async Task<IActionResult> Create()
        {
            ViewData["FavorecidoId"] = new SelectList(_mapper.Map<IEnumerable<FavorecidoViewModel>>(await _favorecidoRepository.ObterTodos()), "Id", "NomeFantasia");
            ViewData["CategoriaId"] = new SelectList(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos()), "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adicionar-lancamento")]
        [ClaimsAuthorize("Lancamento", "Adicionar")]
        public async Task<IActionResult> Create(LancamentoViewModel lancamentoViewModel)
        {
            if (!ModelState.IsValid) return View(lancamentoViewModel);

            DateTime novaDtaVencimento = lancamentoViewModel.DataVencimento;

            for (int i = 0; i <= lancamentoViewModel.QtdeParcela; i++)
            {
                lancamentoViewModel.Ativo = true;
                lancamentoViewModel.DataCadastro = DateTime.Now;
                lancamentoViewModel.DataVencimento = novaDtaVencimento.AddMonths(i);

                var lancamento = _mapper.Map<Lancamento>(lancamentoViewModel);

                await _lancamentoRepository.Adicionar(lancamento);
            }

            TempData["MensagemSucesso"] = "Salvo com sucesso";
            return RedirectToAction(nameof(Index));
        }

        [Route("meu-resumo")]
        public IActionResult Resumo()
        {
            return View();
        }

        public async Task<IActionResult> ConsultarResumo(DateTime dataInicio, DateTime dataFim)
        {
            dataInicio = Convert.ToDateTime(dataInicio.ToString("dd/MM/yyyy"));
            dataFim = Convert.ToDateTime(dataFim.ToString("dd/MM/yyyy"));

            var despesa = _mapper.Map<IEnumerable<LancamentoViewModel>>(await _lancamentoRepository
                                             .ObterTodosLancamentosPorPeriodoDespesas(dataInicio, dataFim))
                                                 .Sum(a => a.Valor);

            var receita = _mapper.Map<IEnumerable<LancamentoViewModel>>(await _lancamentoRepository
                                           .ObterTodosLancamentosPorPeriodoReceitas(dataInicio, dataFim))
                                               .Sum(a => a.Valor);
            return Json(new
            {
                totalDespesa = Convert.ToDecimal(despesa).ToString("C"),
                totalReceita = Convert.ToDecimal(receita).ToString("C"),
                totalSaldo = (Convert.ToDecimal(receita) - Convert.ToDecimal(despesa)).ToString("C")
            });
        }

        [ClaimsAuthorize("Lancamento", "Editar")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var lancamentoViewModel = _mapper.Map<LancamentoViewModel>(await _lancamentoRepository.ObterPorId(id));

            ViewData["FavorecidoId"] = new SelectList(_mapper.Map<IEnumerable<FavorecidoViewModel>>(await _favorecidoRepository.ObterTodos()), "Id", "NomeFantasia",lancamentoViewModel.FavorecidoId);
            ViewData["SubCategoriaId"] = new SelectList(_mapper.Map<IEnumerable<SubCategoriaViewModel>>(await _subCategoriaRepository.ObterTodos()), "Id", "Nome", lancamentoViewModel.SubCategoriaId);

            return View(lancamentoViewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("FavorecidoId,SubCategoriaId,DataVencimento,Descricao,Valor,Id,Ativo")] LancamentoViewModel lancamentoViewModel)
        //{
        //    if (id != lancamentoViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(lancamentoViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LancamentoViewModelExists(lancamentoViewModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["FavorecidoId"] = new SelectList(_context.FavorecidoViewModel, "Id", "Documento", lancamentoViewModel.FavorecidoId);
        //    ViewData["SubCategoriaId"] = new SelectList(_context.SubCategoriaViewModel, "Id", "Nome", lancamentoViewModel.SubCategoriaId);
        //    return View(lancamentoViewModel);
        //}

        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lancamentoViewModel = await _context.LancamentoViewModel
        //        .Include(l => l.Favorecido)
        //        .Include(l => l.SubCategoria)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (lancamentoViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(lancamentoViewModel);
        //}

        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var lancamentoViewModel = await _context.LancamentoViewModel
        //        .Include(l => l.Favorecido)
        //        .Include(l => l.SubCategoria)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (lancamentoViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(lancamentoViewModel);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var lancamentoViewModel = await _context.LancamentoViewModel.FindAsync(id);
        //    _context.LancamentoViewModel.Remove(lancamentoViewModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LancamentoViewModelExists(Guid id)
        //{
        //    return _context.LancamentoViewModel.Any(e => e.Id == id);
        //}
    }
}
