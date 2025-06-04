using FluentAssertions;
using Moq;
using Umio.API.Application.CasosDeUso.Usuarios;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Entidades.Enums;
using Umio.API.Entities.Exceptions;
using Umio.API.TestData.Entidades;

namespace Umio.API.Application.Test.CasosDeUso.Usuarios
{
    public class LoginUsuarioTests
    {
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        private readonly Mock<ITokenService> _mockTokenService;
        private readonly LoginUsuario _loginUsuario;
        private readonly DadosCliente _dadosCliente = new DadosCliente();

        public LoginUsuarioTests()
        {
            _mockClienteRepository = new Mock<IClienteRepository>();
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _mockTokenService = new Mock<ITokenService>();
            _loginUsuario = new LoginUsuario(_mockClienteRepository.Object, _mockUsuarioRepository.Object, _mockTokenService.Object);
        }

        [Fact]
        public async Task GerarToken_DeveRetornarToken_QuandoCredenciaisValidas()
        {
            // Arrange  
            var email = DadosCliente.Email;
            var senha = "ValidPassword@123";
            var clienteId = _dadosCliente.ClienteValido.Id;
            var tokenExpected = "valid_token";

            var cliente = _dadosCliente.ClienteValido;
            var usuario = Usuario.CriarNovoUsuario(senha, clienteId, Provedor.Umio);

            _mockClienteRepository
                .Setup(repo => repo.ListarClientes(null, email, null))
                .ReturnsAsync(new List<Cliente> { cliente });

            _mockUsuarioRepository
                .Setup(repo => repo.BuscarPorClienteIdProvedorId(clienteId, Provedor.Umio))
                .ReturnsAsync(usuario);

            _mockTokenService
                .Setup(service => service.GerarToken(clienteId))
                .Returns(tokenExpected);

            // Act  
            var token = await _loginUsuario.GerarToken(email, senha);

            // Assert  
            token.Should().Be(tokenExpected);
            _mockClienteRepository.Verify(repo => repo.ListarClientes(null, email, null), Times.Once);
            _mockUsuarioRepository.Verify(repo => repo.BuscarPorClienteIdProvedorId(clienteId, Provedor.Umio), Times.Once);
            _mockTokenService.Verify(service => service.GerarToken(clienteId), Times.Once);
        }

        [Fact]
        public async Task GerarToken_DeveDispararExcecaoLogin_QuandoClienteNaoForEncontrado()
        {
            // Arrange  
            var email = "user@example.com";
            var senha = "ValidPassword@123";

            _mockClienteRepository
                .Setup(repo => repo.ListarClientes(null, email, null))
                .ReturnsAsync(new List<Cliente>());

            // Act  
            var act = async () => await _loginUsuario.GerarToken(email, senha);

            // Assert  
            await act.Should().ThrowAsync<ExcecaoLogin>();
            _mockClienteRepository.Verify(repo => repo.ListarClientes(null, email, null), Times.Once);
        }

        [Fact]
        public async Task GerarToken_DeveDispararExcecaoLogin_QuandoUsuarioNaoEncontrado()
        {
            // Arrange  
            var email = DadosCliente.Email;
            var senha = "ValidPassword@123";
            var cliente = _dadosCliente.ClienteValido;

            var clienteId = cliente.Id;

            _mockClienteRepository
                .Setup(repo => repo.ListarClientes(null, email, null))
                .ReturnsAsync(new List<Cliente> { cliente });

            _mockUsuarioRepository
                .Setup(repo => repo.BuscarPorClienteIdProvedorId(clienteId, Provedor.Umio))
                .ReturnsAsync((Usuario)null);

            // Act  
            var act = async () => await _loginUsuario.GerarToken(email, senha);

            // Assert  
            await act.Should().ThrowAsync<ExcecaoLogin>();
            _mockClienteRepository.Verify(repo => repo.ListarClientes(null, email, null), Times.Once);
            _mockUsuarioRepository.Verify(repo => repo.BuscarPorClienteIdProvedorId(clienteId, Provedor.Umio), Times.Once);
        }

        [Fact]
        public async Task GerarToken_DeveDispararExcecaoLogin_QuandoSenhaInvalida()
        {
            // Arrange  
            var email = DadosCliente.Email;
            var senha = "InvalidPassword";
            var cliente = _dadosCliente.ClienteValido;

            var clienteId = cliente.Id;

            var usuario = Usuario.CriarNovoUsuario("CorrectPassword@123", clienteId, Provedor.Umio);

            _mockClienteRepository
                .Setup(repo => repo.ListarClientes(null, email, null))
                .ReturnsAsync(new List<Cliente> { cliente });

            _mockUsuarioRepository
                .Setup(repo => repo.BuscarPorClienteIdProvedorId(clienteId, Provedor.Umio))
                .ReturnsAsync(usuario);

            // Act  
            var act = async () => await _loginUsuario.GerarToken(email, senha);

            // Assert  
            await act.Should().ThrowAsync<ExcecaoLogin>();
            _mockClienteRepository.Verify(repo => repo.ListarClientes(null, email, null), Times.Once);
            _mockUsuarioRepository.Verify(repo => repo.BuscarPorClienteIdProvedorId(clienteId, Provedor.Umio), Times.Once);
        }
    }
}
