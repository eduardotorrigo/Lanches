using LanchesMac.Infra.Data;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Services;

public class LancheService
{
    private readonly ApplicationDbContext _context;

    public LancheService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lanche>> FindAll()
    {
        return await _context.Lanches.Include(c => c.Categoria).OrderBy(c => c.Nome).ToListAsync();
    }
    public async Task<Lanche> FindById(int id)
    {
        return await _context.Lanches.Include(c => c.Categoria).FirstOrDefaultAsync(l => l.LancheId == id);
    }
    public async Task Insert (Lanche lanche)
    {
        _context.Add(lanche);
        await _context.SaveChangesAsync();
    }
    public async Task Update(Lanche lanche)
    {
        if (!await _context.Lanches.AnyAsync(l => l.LancheId == lanche.LancheId))
        throw new ArgumentException("Id não encontrado");

        try
        {
            _context.Lanches.Update(lanche);
            await _context.SaveChangesAsync();
        }
        catch(DbUpdateConcurrencyException e)
        {
            throw new Exception(e.Message);
        }
    }
    public async Task Delete(int id)
    {
        var result = await _context.Lanches.FindAsync(id);
        _context.Lanches.Remove(result);
        await _context.SaveChangesAsync();
    }
}
