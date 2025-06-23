using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Pedidos.Interfaces
{
    public interface IListarPedidosFiltro
    {
        Task<IEnumerable<Pedido>> Executar(Guid? clienteId, DateTime? data);
    }
}
