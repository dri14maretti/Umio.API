using Umio.API.Application.CasosDeUso.Clientes.Inputs;
using Umio.API.Application.CasosDeUso.Clientes.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Clientes
{
    public class CriarCliente : ICriarCliente
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public CriarCliente(IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository)
        {
            _clienteRepository = clienteRepository;
            _usuarioRepository = usuarioRepository;
        }
        public async Task<CriarClienteOutput> Executar(CriarClienteInput cliente)
        {
            var clienteCriado = Cliente.CriarNovoCliente(cliente.Nome, cliente.Email, cliente.Telefone);
            var usuarioCriado = Usuario.CriarNovoUsuario(cliente.Senha, clienteCriado.Id, cliente.Provedor);

            var clienteCriadoBanco = await _clienteRepository.CriarCliente(clienteCriado);
            await _usuarioRepository.CriarUsuario(usuarioCriado);
            return new CriarClienteOutput(clienteCriado.Id, clienteCriado.Nome, clienteCriado.Email, clienteCriado.Telefone);
        }
    }
}
