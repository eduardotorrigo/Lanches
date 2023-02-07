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

namespace LanchesMac.Areas_Admin_Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminCategoriasController : Controller
    {
        private readonly CategoriaService _categoriaService;

        public AdminCategoriasController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        // GET: AdminCategorias
        public async Task<IActionResult> Index()
        {

            return View(await _categoriaService.FindAll());
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var result = await _categoriaService.FindById(id.Value);

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
                await _categoriaService.Insert(categoria);
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaService.FindById(id.Value);
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
                    await _categoriaService.Update(categoria);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var categoria = await _categoriaService.FindById(id.Value);
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
            await _categoriaService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
