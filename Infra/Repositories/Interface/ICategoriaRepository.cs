using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories.Interface;

public interface ICategoriaRepository
{
    IEnumerable<Categoria> Categorias { get; }
}
