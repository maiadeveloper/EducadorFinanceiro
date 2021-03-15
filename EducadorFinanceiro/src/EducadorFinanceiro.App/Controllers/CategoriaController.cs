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
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IMapper _mapper;

        public CategoriaController(ICategoriaRepository categoriaRepository, 
                                    IMapper mapper)
        {
            _categoriaRepository = categoriaRepository;
            _mapper = mapper;
        }

        [Route("categorias")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<CategoriaViewModel>>(await _categoriaRepository.ObterTodos()));
        }

        [Route("adicionar-categoria")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("adicionar-categoria")]
        public async Task<IActionResult> Create(CategoriaViewModel categoriaViewModel)
        {
            if (!ModelState.IsValid) return View(categoriaViewModel);

            categoriaViewModel.DataCadastro = DateTime.Now;

            var categoria = _mapper.Map<Categoria>(categoriaViewModel);

            await _categoriaRepository.Adicionar(categoria);

            TempData["MensagemSucesso"] = "Salvo com sucesso";

            return RedirectToAction(nameof(Index));
        }

        //// GET: Categoria/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var categoriaViewModel = await _context.CategoriaViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (categoriaViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(categoriaViewModel);
        //}


        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var categoriaViewModel = await _context.CategoriaViewModel.FindAsync(id);
        //    if (categoriaViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(categoriaViewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Nome,TipoCategoria,Id,Ativo")] CategoriaViewModel categoriaViewModel)
        //{
        //    if (id != categoriaViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(categoriaViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CategoriaViewModelExists(categoriaViewModel.Id))
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
        //    return View(categoriaViewModel);
        //}

        //// GET: Categoria/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var categoriaViewModel = await _context.CategoriaViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (categoriaViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(categoriaViewModel);
        //}


        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var categoriaViewModel = await _context.CategoriaViewModel.FindAsync(id);
        //    _context.CategoriaViewModel.Remove(categoriaViewModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CategoriaViewModelExists(Guid id)
        //{
        //    return _context.CategoriaViewModel.Any(e => e.Id == id);
        //}
    }
}
