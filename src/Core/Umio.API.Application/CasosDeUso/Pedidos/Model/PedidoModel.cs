namespace Umio.API.Application.CasosDeUso.Pedidos.Model
{
    public class PedidoModel
    {
        public IEnumerable<ItemPedidoModel> Itens { get; set; }
        public string Comentarios { get; set; }
        public Guid EnderecoId { get; set; }
        public string CodigoCupom { get; set; }
    }
}
