using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanchesMac.Infra.Data;
using LanchesMac.Models;
using Microsoft.AspNetCore.Authorization;
using LanchesMac.Services;

namespace LanchesMac.Areas_Admin_Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminPedidosController : Controller
    {
        private readonly PedidoService _pedidoService;

        public AdminPedidosController(PedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _pedidoService.FindAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = await _pedidoService.FindById(id.Value);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                await _pedidoService.Insert(pedido);
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = await _pedidoService.FindById(id.Value);
            if (pedido == null)
                return NotFound();

            return View(pedido);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Pedido pedido)
        {
            if (id != pedido.PedidoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _pedidoService.Update(pedido);
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = await _pedidoService.FindById(id.Value);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _pedidoService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
