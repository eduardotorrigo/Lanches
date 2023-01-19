using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models;

public class Lanche : Entity
{
    public int LancheId { get; set; }

    [Required(ErrorMessage = "Informe o nome do lanche")]
    [StringLength(60, MinimumLength = 10, ErrorMessage = "O {0} de ter no mínimo {1} e no máximo {2} caracteres")]
    [Display(Name = "Nome do Lanche")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe a descrição do lanche")]
    [StringLength(120, MinimumLength = 20, ErrorMessage = "O {0} de ter no mínimo {1} e no máximo {2} caracteres")]
    [Display(Name = "Descrição do Lanche")]
    public string DescricaoCurta { get; set; }

    [Required(ErrorMessage = "Informe a descrição detalhada do lanche")]
    [StringLength(200, MinimumLength = 20, ErrorMessage = "O {0} de ter no mínimo {1} e no máximo {2} caracteres")]
    [Display(Name = "Descrição detalhada do Lanche")]
    public string DescricaoLonga { get; set; }

    [Required(ErrorMessage = "Informe o preço do lanche")]
    [Display(Name = "Preço")]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    [Display(Name = "Caminho da imagem")]
    [StringLength(100, ErrorMessage = "O {0} de ter no mínimo {1} e no máximo {2} caracteres")]
    public string ImagemUrl { get; set; }

    [Display(Name = "Caminho da imagem miniatura")]
    [StringLength(100, ErrorMessage = "O {0} de ter no mínimo {1} e no máximo {2} caracteres")]
    public string ImagemThumbnailUrl { get; set; }

    [Display(Name = "Preferido")]
    public bool LanchePreferido { get; set; }

    [Display(Name = "Estoque")]
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
