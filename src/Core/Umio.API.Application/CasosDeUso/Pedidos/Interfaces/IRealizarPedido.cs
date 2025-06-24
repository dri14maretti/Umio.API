using Umio.API.Application.CasosDeUso.Pedidos.Model;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Pedidos.Interfaces
{
    public interface IRealizarPedido
    {
        public Task<Pedido> Executar(PedidoModel pedidoModel, Guid clienteId);
    }
}
