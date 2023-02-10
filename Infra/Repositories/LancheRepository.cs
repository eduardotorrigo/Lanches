using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Infra.Repositories;

public class LancheRepository : ILancheRepository
{
    private readonly ApplicationDbContext _context;

    public LancheRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Lanche> Lanches => _context.Lanches.Where(l => l.EmEstoque == true)
                                                        .Include(c => c.Categoria)
                                                        .OrderBy(c => c.Nome)
                                                        .AsQueryable();

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(l => l.LanchePreferido).Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId)
    {
        return _context.Lanches.Include(c => c.Categoria).FirstOrDefault(l => l.LancheId == lancheId);
    }
    public async Task Insert(Lanche lanche)
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
        catch (DbUpdateConcurrencyException e)
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
