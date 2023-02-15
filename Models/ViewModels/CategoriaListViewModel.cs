namespace LanchesMac.Models.ViewModels;

public class CategoriaListViewModel
{
    public Lanche Lanche { get; set; }
    public ICollection<Categoria> Categorias { get; set; }
    
}
