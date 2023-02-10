using LanchesMac.Infra.Data;
using LanchesMac.Infra.Repositories.Interface;
using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Infra.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly ApplicationDbContext _context;

    public CategoriaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Categoria> Categorias => _context.Categorias.AsNoTracking().AsQueryable();

    public IQueryable<Categoria> FindAll()
    {
        var categorias = _context.Categorias.AsQueryable();
        return categorias;
    }
    public Categoria FindById(int categoriaId)
    {
        return _context.Categorias.FirstOrDefault(c => c.CategoriaId == categoriaId);
    }
    public async Task Insert(Categoria categoria)
    {
        _context.Add(categoria);
        await _context.SaveChangesAsync();
    }
    public async Task Remove(int id)
    {
        var result = await _context.Categorias.FindAsync(id);
        if (result == null)
            throw new ArgumentException("Id não encontrado");

        _context.Remove(result);
        await _context.SaveChangesAsync();
    }
    public async Task Update(Categoria categoria)
    {
        if (!await _context.Categorias.AnyAsync(c => c.CategoriaId == categoria.CategoriaId))
            throw new ArgumentException("Id não encontrado");

        _context.Update(categoria);
        await _context.SaveChangesAsync();
    }
}
