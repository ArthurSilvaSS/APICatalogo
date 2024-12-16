using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICatalogo.Context;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalagoxUnitTests.UnitTests
{
    public class ProdutosUnitTestController
    {
        private IUnitOfWork repository;
        private IMapper mapper;

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
            repository = new UnitOfWork(context);
        }
    }
}
