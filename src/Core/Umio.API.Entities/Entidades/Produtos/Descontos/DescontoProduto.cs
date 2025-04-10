using Umio.API.Entities.Entidades.Produtos;
namespace Umio.API.Entities.Entidades
{
    public class DescontoProduto
    {
        public Guid Id { get; private set; }
        public List<Produto> Produtos { get; private set; } // Produtos que entrarão na promoção
        public decimal ValorTotalDesconto { get; private set; }
        public DateTime Validade { get; private set; }
    }
}