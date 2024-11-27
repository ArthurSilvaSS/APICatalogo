using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repositories;

public class ProdutoRapository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRapository(AppDbContext context) : base(context)
    {
    }

    public async Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosFiltroParams)
    {
        var produtos = await GetAllAsync();
        var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();
        var resulltado = await 
            produtosOrdenados.ToPagedListAsync(produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);
        return resulltado;
    }

    public async Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParams)
    {
        var produtos = await GetAllAsync();

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

        var produtosFiltrados = await
            produtos.ToPagedListAsync(produtosFiltroParams.PageNumber, produtosFiltroParams.PageSize);
        return produtosFiltrados;
    }

    public async Task<IEnumerable<Produto>> GetProdutosProCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();
        var produtosCategoria = produtos.Where(p => p.CategoriaId == id);
        return produtosCategoria;
    }
}
