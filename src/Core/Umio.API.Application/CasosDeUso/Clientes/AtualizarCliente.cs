using Umio.API.Application.CasosDeUso.Clientes.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Clientes
{
    public class AtualizarCliente : IAtualizarCliente
    {
        private readonly IClienteRepository _clienteRepository;

        public AtualizarCliente(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente?> Executar(Guid id, string? nome, string? telefone, string? email, int? pontos)
        {
            var cliente = await _clienteRepository.BuscarClientePorId(id);
            if (cliente == null) return null;

            cliente.AtualizarCliente(nome, telefone, email, pontos);
            await _clienteRepository.AtualizarCliente(cliente);
            return cliente;
        }
    }
}