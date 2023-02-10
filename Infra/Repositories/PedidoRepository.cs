using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

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
    public IQueryable<Pedido> GetPedido => _context.Pedidos.AsQueryable();
    public Pedido GetPedidoById(int id)
    {
        return _context.Pedidos.FirstOrDefault(p => p.PedidoId == id);
    }
    public async Task Insert(Pedido pedido)
    {
        _context.Pedidos.Add(pedido);
        await _context.SaveChangesAsync();
    }
    public async Task Update(Pedido pedido)
    {
        if (!await _context.Pedidos.AnyAsync(p => p.PedidoId == pedido.PedidoId))
            throw new ArgumentException("Id não encontrado");

        try
        {
            _context.Pedidos.Update(pedido);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task Delete(int id)
    {
        var result = await _context.Pedidos.FindAsync(id);
        _context.Pedidos.Remove(result);
        await _context.SaveChangesAsync();
    }
    public Pedido PedidoLanches(int id)
    {
        return _context.Pedidos.Include(p => p.PedidoItens)
                                 .ThenInclude(l => l.Lanche)
                                 .FirstOrDefault(p => p.PedidoId == id);

    }
}
