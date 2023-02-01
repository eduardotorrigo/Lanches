using LanchesMac.Infra.Data;
using LanchesMac.Models;
using LanchesMac.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class CarrinhoComprasController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly CarrinhoCompra _carrinhoCompra;

    public CarrinhoComprasController(ApplicationDbContext context, CarrinhoCompra carrinhoCompra)
    {
        _context = context;
        _carrinhoCompra = carrinhoCompra;
    }

    public IActionResult Index()
    {
        var itens = _carrinhoCompra.GetCarrinhoCompraItens();
        
        _carrinhoCompra.CarrinhoCompraItems = itens;

        var carrinhoCompraVM = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
        CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal()
        };
        return View(carrinhoCompraVM);
    }
    [Authorize]
    public IActionResult AdicionarItemNoCarrinhoCompra (int? lancheId)
    {
        var lancheSelecionado = _context.Lanches.FirstOrDefault(c => c.LancheId == lancheId);
        if (lancheId != null)
            _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);

        return RedirectToAction("Index");
    }
    [Authorize]
    public IActionResult RemoverItemDoCarrinhoCompra (int? lancheId)
    {
        var lancheSelecionado = _context.Lanches.FirstOrDefault(c => c.LancheId == lancheId);

        if (lancheSelecionado != null)
        _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);

        return RedirectToAction("Index");
    }

}
