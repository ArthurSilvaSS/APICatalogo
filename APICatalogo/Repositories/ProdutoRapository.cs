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

    public PagedLitst<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParams)
    {
        var produtos = GetAll().AsQueryable();

        if (produtosFiltroParams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParams.PrecoCriterio))
        {
            if (produtosFiltroParams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos
                    .Where(p => p.Preco > produtosFiltroParams.Preco.Value)
                    .OrderBy((p => p.Preco));
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos
                    .Where(p => p.Preco < produtosFiltroParams.Preco.Value)
                    .OrderBy((p => p.Preco));
            }
            else if (produtosFiltroParams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos
                    .Where(p => p.Preco == produtosFiltroParams.Preco.Value)
                    .OrderBy((p => p.Preco));
            }
        }

        var produtosFiltrados = PagedLitst<Produto>
            .ToPagedLitst(produtos, produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);

        return produtosFiltrados;
    }

    public IEnumerable<Produto> GetProdutosProCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
