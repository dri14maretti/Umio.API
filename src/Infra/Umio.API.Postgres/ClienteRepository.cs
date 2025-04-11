using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Postgres
{
    internal class ClienteRepository : IClienteRepository
    {
        public async Task<bool> CriarCliente(Cliente cliente)
        {
            return true;
        }
    }
}