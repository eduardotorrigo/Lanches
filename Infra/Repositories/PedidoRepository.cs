using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly ApplicationDbContext _context;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoRepository(ApplicationDbContext context, CarrinhoCompra carrinhoCompra)
    {
        _context = context;
        _carrinhoCompra = carrinhoCompra;
    }
    
    public void CriarPedido(Pedido pedido)
    {
        pedido.PedidoEnviado = DateTime.Now;
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;
        foreach (var CarrinhoItem in carrinhoCompraItens)
        {
            var pedidoDetail = new PedidoDetalhe()
            {
                Quantidade = CarrinhoItem.Quantidade,
                LancheId = CarrinhoItem.LancheId,
                PedidoId = pedido.PedidoId,
                Preco = CarrinhoItem.Lanche.Preco
            };
            _context.PedidoDetalhes.Add(pedidoDetail);
        }
        _context.SaveChanges();
    }
}
