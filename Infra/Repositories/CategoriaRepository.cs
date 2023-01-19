using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> Categorias => _context.Categorias;
}
