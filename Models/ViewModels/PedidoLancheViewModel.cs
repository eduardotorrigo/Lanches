namespace LanchesMac.Models.ViewModels;

public class PedidoLancheViewModel
{
    public Pedido Pedido { get; set; }
    public IEnumerable<PedidoDetalhe> PedidoDetalhe { get; set; }
}
