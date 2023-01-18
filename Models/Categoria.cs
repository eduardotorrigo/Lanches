namespace LanchesMac.Models;

public class Categoria : Entity
{
    public int CategoriaId { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public List<Lanche> Lanches { get; set; }
    public Categoria()
    {
    }

    public Categoria(int categoriaId, string nome)
    {
        CategoriaId = categoriaId;
        Nome = nome;
        CreatedBy = "Teste";
        EditedBy = "Teste";
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;
    }
}

