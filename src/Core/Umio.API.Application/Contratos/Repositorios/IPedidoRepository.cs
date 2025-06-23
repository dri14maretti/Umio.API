using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> ListarPedidosPorFiltro(Guid clienteId);
    }
}
