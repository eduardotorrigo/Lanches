using Microsoft.EntityFrameworkCore;
using LanchesMac.Infra.Data;
using LanchesMac.Models;

namespace LanchesMac.Services;

public class CategoriaService
{
    private readonly ApplicationDbContext _context;
    public CategoriaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> FindAll()
    {
        var categorias = await _context.Categorias.ToListAsync();
        return categorias;
    }
    public async Task<Categoria> FindById(int id)
    {
        return await _context.Categorias.FirstOrDefaultAsync(c => c.CategoriaId == id);
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
