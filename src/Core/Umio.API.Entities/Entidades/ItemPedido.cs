using Umio.API.Entities.Entidades.Produtos;
public class ItemPedido
{
    public Guid PedidoId { get; private set; }
    public Guid ProdutoId { get; private set; }
    public Produto? Produto { get; private set; }
    public int Quantidade { get; private set; }

    public ItemPedido(Guid pedidoId, Guid produtoId, int quantidade)
    {
        PedidoId = pedidoId;
        ProdutoId = produtoId;
        Produto = null; 
        Quantidade = quantidade;
    }

    public void AdicionarProduto(Produto produto)
    {
        if (produto == null)
            throw new ArgumentNullException(nameof(produto), "Produto não pode ser nulo.");

        if (produto.Id != ProdutoId)
            throw new InvalidOperationException("O ID do produto não corresponde ao ProdutoId do ItemPedido.");

        Produto = produto;
    }
    public void AtualizarQuantidade(int quantidade)
    {
        if (quantidade <= 0)
            throw new ArgumentException("A quantidade deve ser maior que zero.", nameof(quantidade));

        Quantidade = quantidade;
    }
}