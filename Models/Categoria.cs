using System.ComponentModel.DataAnnotations;

namespace LanchesMac.Models;

public class Categoria : Entity
{
    public int CategoriaId { get; set; }

    [Required(ErrorMessage = "Informe o nome da categoria")]
    [StringLength(60, ErrorMessage = "O tamanho máximo é de 60 caracteres")]
    [Display(Name = "Categoria")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Informe a descrição da categoria")]
    [StringLength(200, ErrorMessage = "O tamanho máximo é de 200 caracteres")]
    [Display(Name = "Descrição")]
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

