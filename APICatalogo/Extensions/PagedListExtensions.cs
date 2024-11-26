using APICatalogo.Models;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Pagination; // Aqui você importa as funções de mapeamento.

namespace APICatalogo.Extensions
{
    public static class PagedListExtensions
    {
        // Método de extensão para converter PagedLitst<Categoria> para IEnumerable<CategoriaDTO>
        public static IEnumerable<CategoriaDTO> ToCategoriaDtoList(this PagedLitst<Categoria> categorias)
        {
            if (categorias == null || !categorias.Any())
            {
                return new List<CategoriaDTO>();
            }

            return categorias.Select(categoria => categoria.ToCategoriaDTO()).ToList();
        }
    }
}