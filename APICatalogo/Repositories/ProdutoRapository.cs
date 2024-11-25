using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public class ProdutoRapository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRapository(AppDbContext context) : base(context)
    {
    }

    public PagedLitst<Produto> GetProdutos(ProdutosParameters produtosParameters)
    {
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados =
            PagedLitst<Produto>
                .ToPagedLitst(produtos, produtosParameters.PageNumber, produtosParameters.PageSize);
        return produtosOrdenados;
    }

    public IEnumerable<Produto> GetProdutosProCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
