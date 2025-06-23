using Umio.API.Application.CasosDeUso.Pedidos.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Pedidos
{
    public class ListarPedidosFiltro : IListarPedidosFiltro
    {
        private readonly IPedidoRepository _pedidoRepository;

        public ListarPedidosFiltro(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<IEnumerable<Pedido>> Executar(Guid? clienteId, DateTime? data)
        {
            throw new NotImplementedException();
        }
    }
}
