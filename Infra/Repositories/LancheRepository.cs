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

    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(l => l.LanchePreferido).Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId)
    {
        return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
    }
}
