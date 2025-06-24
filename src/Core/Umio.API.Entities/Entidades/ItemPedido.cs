using System.ComponentModel.DataAnnotations;
using Umio.API.Entities.Entidades.Produtos;
public class ItemPedido
{
    [Key]
    public Guid PedidoId { get; private set; }
    public Produto? Produto { get; private set; }
    public int Quantidade { get; private set; }
    public string Comentario { get; private set; }

    public ItemPedido(Guid pedidoId, Produto produto, int quantidade, string comentario)
    {
        PedidoId = pedidoId;
        Produto = produto; 
        Quantidade = quantidade;
        Comentario = comentario;
    }
}