
using Umio.API.Application.CasosDeUso.Clientes.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Clientes
{
    public class ListarCliente : IListarCliente
    {
        private readonly IClienteRepository _clienteRepository;

        public ListarCliente(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<IEnumerable<Cliente>> Executar(string? nome = null, string? email = null, Guid? id = null)
        {
            var clientes = await _clienteRepository.ListarClientes(nome, email, id);
            return clientes;
        }

    }
}