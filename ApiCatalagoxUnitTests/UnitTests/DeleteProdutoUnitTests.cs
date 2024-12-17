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
    public class DeleteProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public DeleteProdutoUnitTests(ProdutosUnitTestController fixture)
        {
            _controller = fixture.Controller;
        }

        [Fact]
        public async Task DeleteProdutoById_Return_OkResult()
        {
            var prodId = 3;

            //Act
            var result = await _controller.Delete(prodId) as ActionResult<ProdutoDTO>;

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();

        }


        [Fact]
        public async Task DeleteProdutoById_Return_NotFound()
        {
            //Arrange
            var prodId = 999;

          //Act
           var result = await _controller.Delete(prodId) as ActionResult<ProdutoDTO>;

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NotFoundObjectResult>();
        }
    }
}