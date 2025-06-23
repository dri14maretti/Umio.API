using Umio.API.Application.CasosDeUso.Clientes.Inputs;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface IClienteRepository
    {
        Task<bool> CriarCliente(Cliente cliente);
        Task<Cliente> BuscarClientePorId(Guid clienteId);
        Task<bool> DeletarCliente(Guid clienteId);
        Task<Cliente> AtualizarCliente(Cliente cliente);
        Task<IEnumerable<Cliente>> ListarClientes(string? nome = null, string? email = null, Guid? id = null);
        Task<Cliente?> BuscarPorEmail(string email);

    }
}
