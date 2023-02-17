using LanchesMac.Infra.Data;
using LanchesMac.Models;

namespace LanchesMac.Areas.Admin.Services;

public class GraficoVendasService
{
    private readonly ApplicationDbContext _context;

    public GraficoVendasService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<LancheGrafico> GetVendasLanches(int dias = 360)
    {
        var data = DateTime.Now.AddDays(-dias);

        var lanches = (from pd in _context.PedidoDetalhes
                       join l in _context.Lanches on pd.LancheId equals l.LancheId
                       where pd.Pedido.PedidoEnviado >= data
                       group pd by new { pd.LancheId, l.Nome }
                       into g
                       select new
                       {
                           LancheNome = g.Key.Nome,
                           LanchesQuantidade = g.Sum(q => q.Quantidade),
                           LanchesValorTotal = g.Sum(q => q.Quantidade * q.Preco)
                       });

        var lista = new List<LancheGrafico>();
        foreach(var item in lanches)
        {
            var lancheGrafico = new LancheGrafico();
            lancheGrafico.LancheNome = item.LancheNome;
            lancheGrafico.LanchesQuantidade = item.LanchesQuantidade;
            lancheGrafico.LanchesValorTotal = item.LanchesValorTotal;
            lista.Add(lancheGrafico);
        }
        return lista;
    }
}
