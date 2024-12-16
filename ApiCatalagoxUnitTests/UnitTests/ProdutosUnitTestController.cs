using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICatalogo.Context;
using APICatalogo.Controllers;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalagoxUnitTests.UnitTests
{
    public class ProdutosUnitTestController
    {
        public IUnitOfWork repository;
        public IMapper mapper;

        public ProdutosController Controller { get; }
        public static DbContextOptions<AppDbContext> dbContextOptions { get; }
        public static string connectionString = "Server=localhost;DataBase=ApiCatalogoDB;Uid=root;Pwd=1125";

        static ProdutosUnitTestController()
        {
            dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
                .Options;
        }

        public ProdutosUnitTestController()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProdutoDTOMappingProfile());
            });

            mapper = config.CreateMapper();

            var context = new AppDbContext(dbContextOptions);
            var repository = new UnitOfWork(context);

            Controller = new ProdutosController(repository, mapper);
        }
    }
}
