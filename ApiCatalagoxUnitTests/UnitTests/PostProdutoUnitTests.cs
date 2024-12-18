﻿using System;
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
    public class PostProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public PostProdutoUnitTests(ProdutosUnitTestController fixture)
        {
            _controller = fixture.Controller;
        }

        [Fact]
        public async Task PostProduto_Return_CreatedStatusCode()
        {
            //Arrange
            var novoProdutoDto = new ProdutoDTO
            {
                Nome = "Novo Produto",
                Descricao = "Descricao do Novo Produto",
                Preco = 10.99m,
                ImagemUrl = "ImagemFake1.jpg",
                CategoriaId = 2
            };

            //Act
            var data = await _controller.Post(novoProdutoDto);

            //Assert
            data.Should().BeOfType<CreatedAtRouteResult>()
                .Which.StatusCode.Should().Be(201);

        }

        [Fact]
        public async Task PostProduto_Return_BadRequest()
        {
            ProdutoDTO prod = null;

            //Act
            var data = await _controller.Post(prod);

            //Assert
            data.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);
        }
    }
}
