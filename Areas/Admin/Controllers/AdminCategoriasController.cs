using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Infra.Data;
using LanchesMac.Models;
using System.Security.Claims;
using LanchesMac.Services;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using LanchesMac.Infra.Repositories.Interface;

namespace LanchesMac.Areas_Admin_Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoriasController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public AdminCategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        // GET: AdminCategorias
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var result = _categoriaRepository.Categorias;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = result.Where(c => c.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(result, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public IActionResult Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var result = _categoriaRepository.FindById(id.Value);

            if (result == null)
                return NotFound();

            return View(result);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoriaId,Nome,Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                // var user = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
                categoria.CreatedBy = "Teste";
                categoria.EditedBy = "Teste";
                categoria.CreatedOn = DateTime.Now;
                categoria.EditedOn = DateTime.Now;
                await _categoriaRepository.Insert(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = _categoriaRepository.FindById(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categoria categoria)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    categoria.EditedBy = "Teste2";
                    categoria.EditedOn = DateTime.Now;
                    await _categoriaRepository.Update(categoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoria = _categoriaRepository.FindById(id.Value);
            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriaRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
