using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{

    private readonly IUnitOfWork _ouf;
    private readonly IMapper _mapper;
    public ProdutosController(IUnitOfWork ouf, IMapper mapper)
    {
        _ouf = ouf;
        _mapper = mapper;
    }


    [HttpGet("produtos/{id}")]
    public ActionResult <IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
    {
        var produtos = _ouf.ProdutoRepository.GetProdutosProCategoria(id);

        if (produtos is null)
            return NotFound();

        var produtoDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtoDto);
    }
    //Get Produtos
    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _ouf.ProdutoRepository.GetAll();
        if (produtos is null)
        {
            return NotFound();
        }
        var produtoDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtoDto);
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

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDto);
    }

    //Post Produto
    [HttpPost]
    public ActionResult Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var novoProduto = _ouf.ProdutoRepository.Create(produto);
        _ouf.commit();

        var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProdutoDTOUpdateResponse> Patch(int id,
        JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if (patchProdutoDTO is null || id <= 0)
        {
            return BadRequest();
        }

        var produto = _ouf.ProdutoRepository.Get(c => c.ProdutoId == id);

        if (produto is null)
        {
            return NotFound();
        }

        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);
        patchProdutoDTO.ApplyTo(produtoUpdateRequest, ModelState);

        if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
        {
            return BadRequest(ModelState);
        }

        _mapper.Map(produtoUpdateRequest, produto);
        _ouf.ProdutoRepository.Update(produto);
        _ouf.commit();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    //Put Produto
    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);
        var produtoAtualizado = _ouf.ProdutoRepository.Update(produto);
        _ouf.commit();
        var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDto);
    }

    //Delete Produto
    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _ouf.ProdutoRepository.Get(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto nao encontrado...");
        }

       var produtoDeletado = _ouf.ProdutoRepository.Delete(produto);
        _ouf.commit();

        var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);
        return Ok(produtoDeletadoDto);
    }
}