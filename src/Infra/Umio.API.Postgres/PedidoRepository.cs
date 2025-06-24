using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Postgres
{
    internal class PedidoRepository : IPedidoRepository
    {
        public Task<IEnumerable<Pedido>> ListarPedidosPorFiltro(Guid clienteId)
        {
            throw new NotImplementedException();
        }
    }
}
