using APICatalogo.Context;
using APICatalogo.Models;

namespace APICatalogo.Repositories;

public class ProdutoRapository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRapository(AppDbContext context) : base(context)
    {
    }

    public IEnumerable<Produto> GetProdutosProCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
