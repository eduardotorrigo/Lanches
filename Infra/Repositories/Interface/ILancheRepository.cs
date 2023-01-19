using LanchesMac.Models;

namespace LanchesMac.Infra.Repositories.Interface;

public interface ILancheRepository
{
    IEnumerable<Lanche> Lanches { get; }
    IEnumerable<Lanche> LanchesPreferidos { get; }
    Lanche GetLancheById(int lancheId);
}
