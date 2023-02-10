using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories.Interface;

public interface IPedidoRepository
{
    void CriarPedido(Pedido pedido);
    IQueryable<Pedido> GetPedido { get; }
    Pedido GetPedidoById(int id);
    Task Insert(Pedido pedido);
    Task Update(Pedido pedido);
    Task Delete(int id);
    Pedido PedidoLanches(int id);
}
