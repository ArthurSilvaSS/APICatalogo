using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]

public class CategoriasController : ControllerBase
{
    private readonly ICategoriaRepository _repository;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(ICategoriaRepository repository,ILogger<CategoriasController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    //Get Categorias
    [HttpGet]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
       var categorias = _repository.GetCategorias();
        return Ok(categorias);
    }

    //Get Categorias Id
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _repository.GetCategoria(id);

       if (categoria is null)
        {
            _logger.LogInformation($"Categoria com id= {id} nao encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }

        return Ok(categoria);
    }

    //Post Categorias
    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null)
        {
            _logger.LogWarning($"Dados invalidos...");
            return BadRequest("Dados invalidos");
        }

        var categoriaCriada = _repository.Create(categoria);

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoriaCriada.CategoriaId }, categoriaCriada);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            _logger.LogWarning($"Dados invalidos...");
            return BadRequest("Dados invalidos");
        }
        _repository.Update(categoria);
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _repository.GetCategoria(id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id={id} nao encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _repository.Delete(id);
        return Ok(categoriaExcluida);
    }
}
