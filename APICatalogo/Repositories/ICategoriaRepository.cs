using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;

namespace APICatalogo.Repositores;

public interface ICategoriaRepository : IRepository<Categoria>
{
    PagedLitst<Categoria> GetCategorias(CategoriasParameters categoriasParams);
    PagedLitst<Categoria> GetCategoriaFiltroNome(CategoriasFiltroNome categoriasFiltroNome);
}
