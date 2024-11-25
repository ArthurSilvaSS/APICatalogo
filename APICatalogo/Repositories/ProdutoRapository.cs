using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public class ProdutoRapository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRapository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams)
    {
        return GetAll()
            .OrderBy(p => p.Nome)
            .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
            .Take(produtosParams.PageSize)
            .ToList();
    }

    public IEnumerable<Produto> GetProdutosProCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
