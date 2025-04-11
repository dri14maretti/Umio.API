using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios
{
    internal class CriarCliente : ICriarCliente
    {
        private readonly IClienteRepository _clienteRepository;
        public CriarCliente(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        public async Task<bool> Executar(string nome, string email, string telefone)
        {
            //var cliente = Cliente.CriarNovoCliente(email, nome, telefone);

            //var verificarClienteCriado = await _clienteRepository.CriarCliente(cliente);

            return true;
        }
    }
}
