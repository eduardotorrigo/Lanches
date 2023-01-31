using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers;

public class PedidoController : Controller
{
    private readonly IPedidoRepository _pedidoRepository;
    private readonly CarrinhoCompra _carrinhoCompra;
    

    public PedidoController(CarrinhoCompra carrinhoCompra, IPedidoRepository pedidoRepository)
    {
        _carrinhoCompra = carrinhoCompra;
        _pedidoRepository = pedidoRepository;
    }

    public IActionResult Checkout()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Pedido pedido)
    {
        int totalItensPedido = 0;
        decimal precoTotalPedido = 0.0m;

        //obter os itens do carrinho de compra do cliente
        List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.CarrinhoCompraItems = items;

        //verificando se existem itens de pedido
        if (_carrinhoCompra.CarrinhoCompraItems.Count == 0)
        {
            ModelState.AddModelError("", "Seu carrinho está vazio, que tal incuir um lanche?");
        }

        // calcular o total de itens e o total do pedido
        foreach (var item in items)
        {
            totalItensPedido += item.Quantidade;
            precoTotalPedido += (item.Lanche.Preco * item.Quantidade);
        }

        //atribui os valores obtidos ao pedido
        pedido.TotalItensPedido = totalItensPedido;
        pedido.PedidoTotal = precoTotalPedido;
        // validar os dados do pedido
        if(ModelState.IsValid)
        {
            // criar o pedido e os detalhes
            _pedidoRepository.CriarPedido(pedido);

            //definir mensagem ao cliente
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();

            //limpar o carrinho do cliente
            _carrinhoCompra.LimparCarrinho();

            //exibie a view com dados do cliente e do pedido
            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }
        return View(pedido);
    }

}
