using Umio.API.Application.CasosDeUso.Usuarios.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.Entities.Entidades.Enums;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Application.CasosDeUso.Usuarios
{
    public class LoginUsuario : ILoginUsuario
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ITokenService _tokenService;

        public LoginUsuario(IClienteRepository clienteRepository, IUsuarioRepository usuarioRepository, ITokenService tokenService)
        {
            this._clienteRepository = clienteRepository;
            this._usuarioRepository = usuarioRepository;
            this._tokenService = tokenService;
        }
        public async Task<string> GerarToken(string email, string senha)
        {
            var cliente = (await _clienteRepository.ListarClientes(email: email)).First();
            if (cliente == null) throw new ExcecaoLogin();

            var usuario = await _usuarioRepository.BuscarPorClienteIdProvedorId(cliente.Id, Provedor.Umio);
            if (usuario == null) throw new ExcecaoLogin();

            if (!usuario.ValidarSenha(senha)) throw new ExcecaoLogin();

            var token = _tokenService.GerarToken(cliente.Id);

            return token;
        }
    }
}
