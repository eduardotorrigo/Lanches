using LanchesMac.Infra.Data;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Services;

public class PedidoService
{
    private readonly ApplicationDbContext _context;

    public PedidoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Pedido>> FindAll()
    {
        return await _context.Pedidos.ToListAsync();
    }
    public async Task<Pedido> FindById(int id)
    {
        return await _context.Pedidos.FirstOrDefaultAsync(p => p.PedidoId == id);
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
}
