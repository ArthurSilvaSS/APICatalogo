using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class ProdutoRapository : IProdutoRepository
{
    //Instancia do contenxto
    private readonly AppDbContext _context;

    public ProdutoRapository(AppDbContext context)
    {
        _context = context;
    }

    //Logica de acesso a dados

    //Get Produtos
    public IQueryable<Produto> GetProdutos()
    {
        return _context.Produtos;
    }

    //Get Produto Id
    public Produto GetProduto(int id)
    {
        var protudo = _context.Produtos.FirstOrDefault(p=> p.ProdutoId == id);

        if (protudo is null)
        {
            throw new InvalidCastException("Produto e null");
        }

        return protudo;
    }

    //Post Produto
    public Produto Create(Produto produto)
    {
        if (produto is null)
        {
            throw new InvalidCastException("Produto e null");
        }

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return produto;
    }

    //Put Produto
    public bool Update(Produto produto)
    {
        if (produto is null)
        {
            throw new InvalidCastException("Produto e null");
        }

        if(_context.Produtos.Any(p=>p.ProdutoId==produto.ProdutoId))
        {
            _context.Produtos.Update(produto);
            _context.SaveChanges();

            return true;
        }
        return false;
    }

    //Delete Produto
    public bool Delete(int id)
    {
        var produto = _context.Produtos.Find(id);
        if (produto is not null)
        {
            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return true;
        }
        return false;
    }

}
