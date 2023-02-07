using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Infra.Data;
using LanchesMac.Models;
using System.Security.Claims;
using LanchesMac.Services;
using LanchesMac.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LanchesMac.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly LancheService _lancheService;
        private readonly CategoriaService _categoriaService;

        public AdminLanchesController(LancheService lancheService, CategoriaService categoriaService)
        {
            _lancheService = lancheService;
            _categoriaService = categoriaService;
        }

        public async Task<IActionResult> Index()
        {
            var lanches = await _lancheService.FindAll();
            return View(lanches);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var lanche = await _lancheService.FindById(id.Value);

            if (lanche == null)
                return NotFound();

            return View(lanche);
        }

        public async Task<IActionResult> Create()
        {
            var categoria = await _categoriaService.FindAll();
            var categoriaViewModel = new CategoriaListViewModel { Categorias = categoria };

            return View(categoriaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lanche lanche)
        {
            if (ModelState.IsValid)
            {
                var user = "teste";//http.User.Identity.Name;
                lanche.CreatedBy = user;
                lanche.EditedBy = user;
                lanche.CreatedOn = DateTime.Now;
                lanche.EditedOn = DateTime.Now;
                await _lancheService.Insert(lanche);
                return RedirectToAction(nameof(Index));
            }
            return View(lanche);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var lanche = await _lancheService.FindById(id.Value);
            if (lanche == null)
                return NotFound();

            List<Categoria> categorias = await _categoriaService.FindAll();
            var categoriaViewModel = new CategoriaListViewModel { Lanche = lanche, Categorias = categorias };
            return View(categoriaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lanche lanche)
        {
            if (id != lanche.LancheId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var categoria = await _categoriaService.FindAll();
                var categoriaViewModel = new CategoriaListViewModel { Lanche = lanche, Categorias = categoria };
                return View(categoriaViewModel);
            }
            try
            {
                var user = "teste";//http.User.Identity.Name;
                lanche.EditedBy = user;
                lanche.EditedOn = DateTime.Now;
                await _lancheService.Update(lanche);

            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: AdminLanches/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var lanche = await _lancheService.FindById(id.Value);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _lancheService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
