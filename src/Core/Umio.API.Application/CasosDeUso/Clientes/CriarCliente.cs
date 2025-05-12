using Umio.API.Application.CasosDeUso.Clientes.Inputs;
using Umio.API.Application.CasosDeUso.Clientes.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Application.Contratos.Servicos;
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
            if (!ValidadorSenhaForte.SenhaForte(cliente.Senha))
                throw new ArgumentException("A senha deve conter pelo menos 6 caracteres, uma letra maiúscula, uma minúscula, um número e um caractere especial.");
                
            var clienteCriado = await _clienteRepository.CriarCliente(cliente);
            await _usuarioRepository.CriarUsuario(clienteCriado.Id, cliente.Senha, cliente.Provedor);
            return new CriarClienteOutput(clienteCriado.Id, clienteCriado.Nome, clienteCriado.Email, clienteCriado.Telefone);
        }
    }
}
