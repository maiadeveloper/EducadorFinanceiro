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
    public class SubCategoriaController : Controller
    {
        private readonly ISubCategoriaRepository _subCategoriaRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public SubCategoriaController(ISubCategoriaRepository subCategoriaRepository,
                                      ICategoriaRepository categoriaRepository,
                                      IMapper mapper)
        {
            _subCategoriaRepository = subCategoriaRepository;
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [Route("subcategorias")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SubCategoriaViewModel>>(await _subCategoriaRepository.ObterSubcategorias()));
        }

        [Route("adicionar-subcategoria")]
        public async Task<IActionResult> Create()
        {
            var subCategoriaViewModel = await CarregaCategorias(new SubCategoriaViewModel());
            return View(subCategoriaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adicionar-subcategoria")]
        public async Task<IActionResult> Create(SubCategoriaViewModel subCategoriaViewModel)
        {
            if (!ModelState.IsValid) return View(subCategoriaViewModel);

            subCategoriaViewModel.DataCadastro = DateTime.Now;

            var subCategoria = _mapper.Map<SubCategoria>(subCategoriaViewModel);

            await _subCategoriaRepository.Adicionar(subCategoria);

            return RedirectToAction(nameof(Index));
        }

        public async Task<JsonResult> ObterSubcategoriasPorCategoriaId(Guid id)
        {
            var subCategorias = _mapper.Map<IEnumerable<SubCategoriaViewModel>>(await _subCategoriaRepository.ObterSubcategoriasPorCategoriaId(id));
            return Json(new SelectList(subCategorias.OrderBy(s => s.Nome), "Id", "Nome"));
        }

        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var subCategoriaViewModel = await _context.SubCategoriaViewModel
        //        .Include(s => s.Categoria)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (subCategoriaViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(subCategoriaViewModel);
        //}

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var subCategoriaViewModel = await _context.SubCategoriaViewModel.FindAsync(id);
        //    if (subCategoriaViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["CategoriaId"] = new SelectList(_context.CategoriaViewModel, "Id", "Nome", subCategoriaViewModel.CategoriaId);
        //    return View(subCategoriaViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Nome,CategoriaId,Id,Ativo")] SubCategoriaViewModel subCategoriaViewModel)
        //{
        //    if (id != subCategoriaViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(subCategoriaViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!SubCategoriaViewModelExists(subCategoriaViewModel.Id))
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
        //    ViewData["CategoriaId"] = new SelectList(_context.CategoriaViewModel, "Id", "Nome", subCategoriaViewModel.CategoriaId);
        //    return View(subCategoriaViewModel);
        //}

        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var subCategoriaViewModel = await _context.SubCategoriaViewModel
        //        .Include(s => s.Categoria)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (subCategoriaViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(subCategoriaViewModel);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var subCategoriaViewModel = await _context.SubCategoriaViewModel.FindAsync(id);
        //    _context.SubCategoriaViewModel.Remove(subCategoriaViewModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool SubCategoriaViewModelExists(Guid id)
        //{
        //    return _context.SubCategoriaViewModel.Any(e => e.Id == id);
        //}

        private async Task<SubCategoriaViewModel> CarregaCategorias(SubCategoriaViewModel subCategoriaViewModel)
        {
            subCategoriaViewModel.Categorias = _mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos());
            return subCategoriaViewModel;
        }
    }
}
