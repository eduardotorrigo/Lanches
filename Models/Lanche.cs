namespace LanchesMac.Models;

public class Lanche : Entity
{
    public int LancheId { get; set; }
    public string Nome { get; set; }
    public string DescricaoCurta { get; set; }
    public string DescricaoLonga { get; set; }
    public decimal Preco { get; set; }
    public string ImagemUrl { get; set; }
    public string ImagemThumbnailUrl { get; set; }
    public bool LanchePreferido { get; set; }
    public bool EmEstoque { get; set; }
    public int CategoriaId { get; set; }
    public virtual Categoria Categoria { get; set; }

    public Lanche()
    {
    }

    public Lanche(int lancheId, string nome, string descricaoCurta, string descricaoLonga, decimal preco, string imagemUrl, string imagemThumbnailUrl, bool lanchePreferido, bool emEstoque, int categoriaId)
    {
        LancheId = lancheId;
        Nome = nome;
        DescricaoCurta = descricaoCurta;
        DescricaoLonga = descricaoLonga;
        Preco = preco;
        ImagemUrl = imagemUrl;
        ImagemThumbnailUrl = imagemThumbnailUrl;
        LanchePreferido = lanchePreferido;
        EmEstoque = emEstoque;
        CategoriaId = categoriaId;
        CreatedBy = "Teste";
        EditedBy = "Teste";
        CreatedOn = DateTime.UtcNow;
        EditedOn = DateTime.UtcNow;
    }
}
