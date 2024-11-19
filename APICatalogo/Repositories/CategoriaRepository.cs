using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositores;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    //Instancia do contenxto
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    //Logica de acesso a dados

    //Get Categorias
    public IEnumerable<Categoria> GetCategorias()
    {
       return _context.Categorias.ToList();
    }

    //Get Categorias Id
    public Categoria GetCategoria(int id)
    {
        return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
    }

    //Post Categoria
    public Categoria Create(Categoria categoria)
    {
        if(categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return categoria;
    }

    //Put Categorias
    public Categoria Update(Categoria categoria)
    {
        if (categoria is null)
            throw new ArgumentNullException(nameof(categoria));

        _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        _context.SaveChanges();

        return categoria;
    }
}
