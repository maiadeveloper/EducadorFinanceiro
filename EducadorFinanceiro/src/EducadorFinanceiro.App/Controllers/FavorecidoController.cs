using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EducadorFinanceiro.App.Data;
using EducadorFinanceiro.App.ViewModels;
using EducadorFinanceiro.Business.Interfaces;
using AutoMapper;
using EducadorFinanceiro.Business.Models;

namespace EducadorFinanceiro.App.Controllers
{
    public class FavorecidoController : Controller
    {
        private readonly IFavorecidoRepository  _favorecidoRepository;
        private readonly IMapper _mapper;

        public FavorecidoController(IFavorecidoRepository favorecidoRepository,
                                    IMapper mapper)
        {
            _favorecidoRepository = favorecidoRepository;
            _mapper = mapper;
        }

        [Route("favorecidos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FavorecidoViewModel>>(await _favorecidoRepository.ObterTodos()));
        }

        [Route("adicionar-favorecido")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adicionar-favorecido")]
        public async Task<IActionResult> Create(FavorecidoViewModel favorecidoViewModel)
        {
            if (!ModelState.IsValid) return View(favorecidoViewModel);

            favorecidoViewModel.DataCadastro = DateTime.Now;
            favorecidoViewModel.Documento = favorecidoViewModel.Documento.Replace(".", "").Replace("/", "").Replace("-", "");

            var favorecido = _mapper.Map<Favorecido>(favorecidoViewModel);

            await _favorecidoRepository.Adicionar(favorecido);

            TempData["MensagemSucesso"] = "Salvo com sucesso";

            return RedirectToAction(nameof(Index));
        }

        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var favorecidoViewModel = await _context.FavorecidoViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (favorecidoViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(favorecidoViewModel);
        //}

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var favorecidoViewModel = await _context.FavorecidoViewModel.FindAsync(id);
        //    if (favorecidoViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(favorecidoViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("TipoFavorecido,NomeFantasia,RazaoSocial,Documento,Id,Ativo")] FavorecidoViewModel favorecidoViewModel)
        //{
        //    if (id != favorecidoViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(favorecidoViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FavorecidoViewModelExists(favorecidoViewModel.Id))
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
        //    return View(favorecidoViewModel);
        //}

        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var favorecidoViewModel = await _context.FavorecidoViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (favorecidoViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(favorecidoViewModel);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var favorecidoViewModel = await _context.FavorecidoViewModel.FindAsync(id);
        //    _context.FavorecidoViewModel.Remove(favorecidoViewModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool FavorecidoViewModelExists(Guid id)
        //{
        //    return _context.FavorecidoViewModel.Any(e => e.Id == id);
        //}
    }
}
