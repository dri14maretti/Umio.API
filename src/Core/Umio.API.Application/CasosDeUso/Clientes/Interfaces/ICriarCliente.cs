
using Umio.API.Application.CasosDeUso.Clientes.Inputs;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Clientes.Interfaces
{
    public interface ICriarCliente
    {
        public Task<CriarClienteOutput> Executar(CriarClienteInput cliente);
    }
}
