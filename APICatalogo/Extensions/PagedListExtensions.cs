using APICatalogo.Models;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
using APICatalogo.Pagination; 

namespace APICatalogo.Extensions
{
    public static class PagedListExtensions
    {
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