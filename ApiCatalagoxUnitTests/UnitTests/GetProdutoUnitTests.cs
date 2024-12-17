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
    public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public GetProdutoUnitTests(ProdutosUnitTestController fixture)
        {
            _controller = fixture.Controller;
        }

        [Fact]
        public async Task GetProdutoById_OKResult()
        {
            //Arrange
            var prodId = 1;

            //Act
            var data = await _controller.Get(prodId);

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetProdutoById_Return_NotFound()
        {
            //Arrange
            var prodId = 999;

            //Act
            var data = await _controller.Get(prodId);

            //Assert
            data.Result.Should().BeOfType<NotFoundObjectResult>()
                .Which.StatusCode.Should().Be(404);
        }

        //[Fact]
        //public async Task GetProdutoById_Return_BadRequest()
        //{
        //    //Arrange
        //    var prodId = -1;

        //    //Act
        //    var data = await _controller.Get(prodId);

        //    //Assert
        //    data.Result.Should().BeOfType<BadRequestObjectResult>()
        //        .Which.StatusCode.Should().Be(400);
        //}

        [Fact]
        public async Task GetProduto_Return_ListOfProdutoDTO()
        {

            //Act
            var data = await _controller.Get();

            //Assert
            data.Result.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>()
                .And.NotBeNull();
        }

        //[Fact]
        //public async Task GetProduto_Return_BadRequestResult()
        //{

        //    //Act
        //    var data = await _controller.Get();

        //    //Assert
        //    data.Result.Should().BeOfType<BadRequestResult>();

        //}

    }
}
