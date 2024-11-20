using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{

    private readonly IUnitOfWork _ouf;
    public ProdutosController(IUnitOfWork ouf)
    {
        _ouf = ouf;
    }


    [HttpGet("produtos/{id}")]
    public ActionResult <IEnumerable<Produto>> GetProdutosCategoria(int id)
    {
        var produtos = _ouf.ProdutoRepository.GetProdutosProCategoria(id);

        if (produtos is null)
            return NotFound();

        return Ok(produtos);
    }
    //Get Produtos
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _ouf.ProdutoRepository.GetAll();
        if (produtos is null)
        {
            return NotFound();
        }

        return Ok(produtos);
    }

    //Get ProdutosId
    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<Produto> Get(int id)
    {
        var produto = _ouf.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        return Ok(produto);
    }

    //Post Produto
    [HttpPost]
    public ActionResult Post(Produto produto)
    {
        if (produto is null)
            return BadRequest();

        var novoProduto = _ouf.ProdutoRepository.Create(produto);
        _ouf.commit();

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);
    }

    //Put Produto
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if (id != produto.ProdutoId)
        {
            return BadRequest();
        }

        var produtoAtualizado = _ouf.ProdutoRepository.Update(produto);
        _ouf.commit();

        return Ok(produtoAtualizado);
    }

    //Delete Produto
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _ouf.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto nao encontrado...");
        }

       var produtoDeletado = _ouf.ProdutoRepository.Delete(produto);
        _ouf.commit();

        return Ok(produtoDeletado);
    }
}