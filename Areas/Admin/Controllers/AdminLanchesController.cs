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
using LanchesMac.Infra.Repositories.Interface;
using ReflectionIT.Mvc.Paging;

namespace LanchesMac.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly ILancheRepository _lancheRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public AdminLanchesController(ILancheRepository lancheRepository, ICategoriaRepository categoriaRepository)
        {
            _lancheRepository = lancheRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var result = _lancheRepository.Lanches;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(l => l.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var lanche = _lancheRepository.GetLancheById(id.Value);

            if (lanche == null)
                return NotFound();

            return View(lanche);
        }

        public IActionResult Create()
        {
            var categoria = _categoriaRepository.Categorias.ToList();
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
                await _lancheRepository.Insert(lanche);
                return RedirectToAction(nameof(Index));
            }
             var categoria = _categoriaRepository.Categorias.ToList();
            var categoriaViewModel = new CategoriaListViewModel {Lanche = lanche,  Categorias = categoria };

            return View(categoriaViewModel);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var lanche = _lancheRepository.GetLancheById(id.Value);
            if (lanche == null)
                return NotFound();

            List<Categoria> categorias = _categoriaRepository.Categorias.ToList();
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
                var categoria = _categoriaRepository.Categorias.ToList();
                var categoriaViewModel = new CategoriaListViewModel { Lanche = lanche, Categorias = categoria };
                return View(categoriaViewModel);
            }
            try
            {
                var user = "teste";//http.User.Identity.Name;
                lanche.EditedBy = user;
                lanche.EditedOn = DateTime.Now;
                await _lancheRepository.Update(lanche);

            }
            catch (DbUpdateConcurrencyException)
            {
            }
            return RedirectToAction(nameof(Index));

        }

        // GET: AdminLanches/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var lanche = _lancheRepository.GetLancheById(id.Value);
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
            await _lancheRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
