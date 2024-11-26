using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositores;

namespace APICatalogo.Repositories;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbContext context) : base(context)
    {
    }

    public PagedLitst<Categoria> GetCategorias(CategoriasParameters categoriasParameters)
    {
        var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();

        var categoriasOrdenadas = PagedLitst<Categoria>.ToPagedLitst(categorias,
            categoriasParameters.PageNumber, categoriasParameters.PageSize);

        return categoriasOrdenadas;
    }
}
