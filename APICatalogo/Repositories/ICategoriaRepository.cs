using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;
using X.PagedList;

namespace APICatalogo.Repositores;

public interface ICategoriaRepository : IRepository<Categoria>
{
   Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParams);
   Task<IPagedList<Categoria>> GetCategoriaFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroNome);
}
