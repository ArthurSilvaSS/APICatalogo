﻿using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositores;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APICatalogo.Extensions;


namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]

public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly ILogger<CategoriasController> _logger;

    public CategoriasController(ILogger<CategoriasController> logger, IUnitOfWork uof)
    {
        _logger = logger;
        _uof = uof;
    }

    //Get Categorias
    [HttpGet]
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
       var categorias = _uof.CategoriaRepository.GetAll();
       var categoriasDto = categorias.ToCategoriaDtos();

        return Ok(categoriasDto);
    }

    //Get Categorias Id
    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c=> c.CategoriaId == id);

       if (categoria is null)
        {
            _logger.LogInformation($"Categoria com id= {id} nao encontrada...");
            return NotFound($"Categoria com id= {id} não encontrada...");
        }

        var categoriaDto = categoria.ToCategoriaDTO();

        return Ok(categoriaDto);
    }

    //Post Categorias
    [HttpPost]
    public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null)
        {
            _logger.LogWarning($"Dados invalidos...");
            return BadRequest("Dados invalidos");
        }

        var categoria = categoriaDto.ToCategoria();

        var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
        _uof.commit();

        var novaCategoriaDto = categoriaCriada.ToCategoriaDTO();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = novaCategoriaDto.CategoriaId }, novaCategoriaDto);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            _logger.LogWarning($"Dados invalidos...");
            return BadRequest("Dados invalidos");
        }

        var categoria = categoriaDto.ToCategoria();
        var categoriaAtualizada = _uof.CategoriaRepository.Update(categoria);
        _uof.commit();

        var categoriaAtualizadaDto = categoriaAtualizada.ToCategoriaDTO();
        return Ok(categoriaAtualizadaDto);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria is null)
        {
            _logger.LogWarning($"Categoria com id={id} nao encontrada...");
            return NotFound($"Categoria com id={id} não encontrada...");
        }

        var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
        _uof.commit();

        var categoriaExcluidaDto = categoriaExcluida.ToCategoriaDTO();

        return Ok(categoriaExcluida);
    }
}
