using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using X.PagedList;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class ProdutosController : ControllerBase
{

    private readonly IUnitOfWork _ouf;
    private readonly IMapper _mapper;
    public readonly IUnitOfWork repository;
    public readonly IMapper mapper;

    public ProdutosController(IUnitOfWork ouf, IMapper mapper)
    {
        _ouf = ouf;
        _mapper = mapper;
    }


    [HttpGet("produtos/{id}")]
    public async Task<ActionResult <IEnumerable<ProdutoDTO>>> GetProdutosCategoria(int id)
    {
        var produtos = await _ouf.ProdutoRepository.GetProdutosProCategoriaAsync(id);

        if (produtos is null)
            return NotFound();

        var produtoDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtoDto);
    }

    [HttpGet("pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
    {
        var produtos = await _ouf.ProdutoRepository.GetProdutosAsync(produtosParameters);
        return ObterProdutos(produtos);
    }

    [HttpGet("filter/preco/pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>>GetProdutosFilterPreco([FromQuery] ProdutosFiltroPreco produtoFilterParameters)
    {
        var produtos = await _ouf.ProdutoRepository.GetProdutosFiltroPrecoAsync(produtoFilterParameters);
        return ObterProdutos(produtos);

    }
    private ActionResult<IEnumerable<ProdutoDTO>> ObterProdutos(IPagedList<Produto> produtos)
    {
        var metadata = new
        {
            produtos.Count,
            produtos.PageSize,
            produtos.PageCount,
            produtos.TotalItemCount,
            produtos.HasNextPage,
            produtos.HasPreviousPage
        };

        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    //Get Produtos
    [HttpGet]
    [Authorize(Policy = "UserOnly")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
    {
        var produtos = await _ouf.ProdutoRepository.GetAllAsync();
        if (produtos is null)
        {
            return NotFound();
        }
        var produtoDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtoDto);
    }

    //Get ProdutosId
    [HttpGet("{id}", Name = "ObterProduto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Produto>> Get(int id)
    {
        var produto = await _ouf.ProdutoRepository.GetAsync(c => c.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDto);
    }

    //Post Produto
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);

        var novoProduto = _ouf.ProdutoRepository.Create(produto);
        await _ouf.commitAsync();

        var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);

        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProduto);
    }

    [HttpPatch("{id}/UpdatePartial")]
    public async Task<ActionResult<ProdutoDTOUpdateResponse>> Patch(int id,
        JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDTO)
    {
        if (patchProdutoDTO is null || id <= 0)
        {
            return BadRequest();
        }

        var produto = await _ouf.ProdutoRepository.GetAsync(c => c.ProdutoId == id);

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
        await _ouf.commitAsync();

        return Ok(_mapper.Map<ProdutoDTOUpdateResponse>(produto));
    }

    //Put Produto
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);
        var produtoAtualizado = _ouf.ProdutoRepository.Update(produto);
        await _ouf.commitAsync();
        var produtoAtualizadoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);

        return Ok(produtoAtualizadoDto);
    }

    //Delete Produto
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        var produto = await _ouf.ProdutoRepository.GetAsync(p => p.ProdutoId == id);

        if (produto is null)
        {
            return NotFound("Produto nao encontrado...");
        }

        var produtoDeletado = _ouf.ProdutoRepository.Delete(produto);
        await _ouf.commitAsync();

        var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);
        return Ok(produtoDeletadoDto);
    }

}