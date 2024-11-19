using System.Diagnostics.Eventing.Reader;
using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;
    public CategoriasController(AppDbContext context,
        ILogger<CategoriasController> logger)
        
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet("produtos")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        _logger.LogInformation("=========GET api/categorias/produtos===========");
        //return _context.Categorias.Include(p=> p.Produtos).ToList();
        return _context.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        return _context.Categorias.AsNoTracking().ToList();
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        _logger.LogInformation($"=========GET api/categorias/id = {id}===========");

        throw new ArgumentException("Ocorreu um erro no tratamento do request");
 

       var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

       if (categoria == null)
      {
            _logger.LogInformation($"=========GET api/categorias/id = {id} NOT FOUND===========");
            return NotFound("Categoria não encontrada...");
        }
        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
            return BadRequest();

        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return Ok(categoria);
    }
}
