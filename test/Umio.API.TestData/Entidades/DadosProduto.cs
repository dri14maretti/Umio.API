using Umio.API.Entities.Entidades.Produtos;

namespace Umio.API.TestData.Entidades
{
    public static class DadosProduto
    {
        public static int Id => 1;
        public static string Nome => "Produto Exemplo";
        public static decimal Preco => 99.99m;
        public static string Descricao => "Descrição do Produto Exemplo";
        public static string Imagem => "https://example.com/imagem-produto.jpg";
        public static int CategoriaId => 10;
        public static bool Ativo => true;

        public static Produto ProdutoValido => new(
            Id,
            Nome,
            Preco,
            Descricao,
            Imagem,
            CategoriaId,
            Ativo
        );

        public static IEnumerable<Produto> ListaProdutos = new List<Produto>()
        {
            DadosProduto.ProdutoValido,
            new Produto(2, "Produto Teste", 49.99m, "Descrição do Produto Teste", "https://example.com/imagem-produto-teste.jpg", 20, true),
        }; 
    }
}
