namespace Umio.API.Application.CasosDeUso.Pedidos.Model
{
    public class ItemPedidoModel
    {
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public IEnumerable<int> AdicionaisId { get; set; }
        public IEnumerable<int> AcompanhamentosId { get; set; }
    }
}
