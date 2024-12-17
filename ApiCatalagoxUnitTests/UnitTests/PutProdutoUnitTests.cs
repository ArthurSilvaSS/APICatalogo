using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICatalogo.Controllers;
using APICatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace ApiCatalagoxUnitTests.UnitTests
{
    public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public PutProdutoUnitTests(ProdutosUnitTestController fixture)
        {
            _controller = fixture.Controller;
        }

        [Fact]
        public async Task PutProduto_Return_OkResult()
        {
            //Arrange
            var prodId = 10;

            var updateProdutoDto = new ProdutoDTO
            {
                ProdutoId = prodId,
                Nome = "Produto Atualizado - Testes",
                Descricao = "Descricao Teste",
                ImagemUrl = "imagem1.jpg",
                CategoriaId = 2
            };

            //Act
            var result = await _controller.Put(prodId, updateProdutoDto) as ActionResult<ProdutoDTO>;

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task PutProduto_Return_BadRequest()
        {
            //Arrange
            var prodId = 1000;

            var meuProduto = new ProdutoDTO
            {
                ProdutoId = 10,
                Nome = "Produto Atualizado - Testes",
                Descricao = "Minha Descricao alterada",
                ImagemUrl = "imagem11.jpg",
                CategoriaId = 2
            };

            //Act
            var data = await _controller.Put(prodId, meuProduto);

            //Assert
            data.Result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }
    }
}
