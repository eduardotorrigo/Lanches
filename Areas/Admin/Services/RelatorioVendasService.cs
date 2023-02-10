using LanchesMac.Infra.Data;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Areas.Admin.Services;

public class RelatorioVendasService
{
    private readonly ApplicationDbContext _context;

    public RelatorioVendasService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var result = from obj in _context.Pedidos select obj;

        if (minDate.HasValue)
        {
            result = result.Where(p => p.PedidoEnviado >= minDate.Value);
        }
        if (maxDate.HasValue)
        {
            result = result.Where(p => p.PedidoEnviado <= maxDate.Value);
        }

        return await result
                        .Include(p => p.PedidoItens)
                        .ThenInclude(l => l.Lanche)
                        .OrderByDescending(x => x.PedidoEnviado)
                        .ToListAsync();
    }
}
