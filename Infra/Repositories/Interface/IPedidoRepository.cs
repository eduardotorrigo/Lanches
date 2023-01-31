using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories.Interface;

public interface IPedidoRepository
{
    void CriarPedido(Pedido pedido);
}
