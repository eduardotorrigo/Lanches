using LanchesMac.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra
{
    private readonly ApplicationDbContext _context;

    public CarrinhoCompra(ApplicationDbContext contexto)
    {
        _context = contexto;
    }
    public Guid CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider service)
    {
        //define uma sessão
        ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        //obtem um serviço do tipo do nosso contexto
        var context = service.GetService<ApplicationDbContext>();

        //obtem ou gera um Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        //atribuir o id do carrinho na Sessão
        session.SetString("CarrinhoId", carrinhoId);

        //retorna o carrinho com o context e o Id atribuido ou obtido
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = Guid.Parse(carrinhoId)

        };

    }
    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(c =>
        c.Lanche.LancheId == lanche.LancheId &&
        c.CarrinhoCompraId == CarrinhoCompraId);

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
        {
            carrinhoCompraItem.Quantidade++;
        }
        _context.SaveChanges();
    }
    public void RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(c =>
        c.Lanche.LancheId == lanche.LancheId &&
        c.CarrinhoCompraId == CarrinhoCompraId);


        if (carrinhoCompraItem != null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
            }
        }
        _context.SaveChanges();
    }
    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItems ??
         (CarrinhoCompraItems =
         _context.CarrinhoCompraItens
        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
        .Include(l => l.Lanche)
        .ToList());
    }
    public void LimparCarrinho()
    {
        var carrinhoItem = _context.CarrinhoCompraItens
        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItem);
        _context.SaveChanges();
    }
    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.CarrinhoCompraItens
        .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
        .Select(c => c.Lanche.Preco * c.Quantidade).Sum();

        return total;
    }

}
