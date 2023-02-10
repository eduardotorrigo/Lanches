using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories.Interface;

public interface ICategoriaRepository
{
    IQueryable<Categoria> Categorias { get; }
    Categoria FindById(int categoriaId);
    Task Insert(Categoria categoria);
    Task Remove(int id);
    Task Update(Categoria categoria);

}
