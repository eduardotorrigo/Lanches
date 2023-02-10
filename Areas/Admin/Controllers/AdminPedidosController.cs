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
using ReflectionIT.Mvc.Paging;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models.ViewModels;

namespace LanchesMac.Areas_Admin_Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]

    public class AdminPedidosController : Controller
    {
        private readonly IPedidoRepository _pedidoRepository;

        public AdminPedidosController(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        // public async Task<IActionResult> Index()
        // {
        //     return View(await _pedidoService.FindAll());
        // }
        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _pedidoRepository.GetPedido;


            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");

            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = _pedidoRepository.GetPedidoById(id.Value);
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
                await _pedidoRepository.Insert(pedido);
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = _pedidoRepository.GetPedidoById(id.Value);
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
                await _pedidoRepository.Update(pedido);
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedido = _pedidoRepository.GetPedidoById(id.Value);
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
            await _pedidoRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult PedidoLanches(int? id)
        {
            if (id == null)
                return NotFound();

            var pedido = _pedidoRepository.PedidoLanches(id.Value);
            if (pedido == null)
            {
                Response.StatusCode = 404;
                return View("PedidoNotFound", id.Value);
            }

            PedidoLancheViewModel pedidoLancheVM = new PedidoLancheViewModel()
            {
                Pedido = pedido,
                PedidoDetalhe = pedido.PedidoItens
        };
            return View(pedidoLancheVM);
        }
    }
   
}
