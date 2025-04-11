using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface IClienteRepository
    {
        public Task<bool> CriarCliente(Cliente cliente);
    }
}
