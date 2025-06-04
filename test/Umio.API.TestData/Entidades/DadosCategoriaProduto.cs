using Umio.API.Entities.Entidades;

namespace Umio.API.TestData.Entidades
{
    public static class DadosCategoriaProduto
    {
        public static CategoriaProduto CategoriaValida1 => new CategoriaProduto(1, "Lanches");

        public static CategoriaProduto CategoriaValida2 => new CategoriaProduto(2, "Saladas");

        public static List<CategoriaProduto> ListaCategorias => new List<CategoriaProduto>
        {
            CategoriaValida1,
            CategoriaValida2
        };
    }
}
