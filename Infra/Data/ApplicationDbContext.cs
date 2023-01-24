using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Infra.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Lanche> Lanches { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Categoria>().HasData(
        new Categoria { CategoriaId = 1, Nome = "Normal", Descricao = "anche feito com ingredientes normais" },
        new Categoria { CategoriaId = 2, Nome = "Natural", Descricao = "Lanche feito com ingredientes integrais e naturais" }
        );

        builder.Entity<Lanche>().HasData(
            new Lanche { LancheId = 1, Nome = "Cheese Salada", DescricaoCurta = "Pão, Hamburger, ovo, presunto, queijo e batata palha", DescricaoLonga = "Delicioso hamburger com ovo frito; presunto e queijo de primeira qualidade acompanhado com batata palha", Preco = 12.50M, ImagemUrl = "~/imagens/produtos/cheesesalada1.jpg", LanchePreferido = false,  EmEstoque = true, CategoriaId = 1, CreatedBy = "teste", CreatedOn = DateTime.Now, EditedBy = "teste", EditedOn = DateTime.Now},
            new Lanche { LancheId = 2, Nome = "Misto Quente", DescricaoCurta = "Pão, presunto, mussarela e tomate", DescricaoLonga = "Delicioso pão francês quentinho na chapa com presunto e mussarela bem servidos com tomate preparado com carinho", Preco = 8.0M, ImagemUrl = "~/imagens/produtos/mistoquente4.jpg", LanchePreferido = false,  EmEstoque = true, CategoriaId = 1, CreatedBy = "teste", CreatedOn = DateTime.Now, EditedBy = "teste", EditedOn = DateTime.Now},
            new Lanche { LancheId = 3, Nome = "Cheese Burger", DescricaoCurta = "Pão, hambúrger, presunto, mussarela e batalha palha", DescricaoLonga = "Pão de hambúrger especial com hambúrger de nossa preparação e presunto e mussarela; acompanha batata palha", Preco = 11.0M, ImagemUrl = "~/imagens/produtos/cheeseburger1.jpg", LanchePreferido = false,  EmEstoque = true, CategoriaId = 1, CreatedBy = "teste", CreatedOn = DateTime.Now, EditedBy = "teste", EditedOn = DateTime.Now},
            new Lanche { LancheId = 4, Nome = "anche Natural Peito Peru", DescricaoCurta = "Pão Integral, queijo branco, peito de peru, cenoura, alface, iogurte", DescricaoLonga = "Pão integral natural com queijo branco, peito de peru e cenoura ralada com alface picado e iorgurte natural", Preco = 15.0M, ImagemUrl = "~/imagens/produtos/lanchenatural.jpg", LanchePreferido = false,  EmEstoque = true, CategoriaId = 2, CreatedBy = "teste", CreatedOn = DateTime.Now, EditedBy = "teste", EditedOn = DateTime.Now}
        );
    }
}
