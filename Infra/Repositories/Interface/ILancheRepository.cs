using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories.Interface;

public interface ILancheRepository
{
    IQueryable<Lanche> Lanches { get; }
    IEnumerable<Lanche> LanchesPreferidos { get; }
    Lanche GetLancheById(int lancheId);
    Task Insert(Lanche lanche);
    Task Update(Lanche lanche);
    Task Delete(int id);
}
