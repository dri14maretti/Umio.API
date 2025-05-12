using Umio.API.Application.CasosDeUso.Clientes.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Clientes
{
    public class DeletarCliente : IDeletarCliente
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public DeletarCliente(IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository)
        {
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Executar(Guid id)
        {
            var cliente = await _clienteRepository.BuscarClientePorId(id);
            if (cliente == null) return false;
            await _usuarioRepository.DeletarPorClienteId(cliente.Id);
            await _clienteRepository.DeletarCliente(cliente.Id);
            return true;
        }
    }
}