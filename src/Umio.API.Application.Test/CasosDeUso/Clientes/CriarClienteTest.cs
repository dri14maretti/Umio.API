using FluentAssertions;
using Moq;
using Umio.API.Application.CasosDeUso.Clientes;
using Umio.API.Application.CasosDeUso.Clientes.Inputs;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Entidades.Enums;

namespace Umio.API.Application.Test.CasosDeUso.Clientes
{
    public class CriarClienteTest
    {
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
        private readonly CriarCliente _criarCliente;

        public CriarClienteTest()
        {
            _mockClienteRepository = new Mock<IClienteRepository>();
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _criarCliente = new CriarCliente(_mockClienteRepository.Object, _mockUsuarioRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveCriarClienteEUsuario_QuandoDadosForemValidos()
        {
            // Arrange
            var input = new CriarClienteInput(
                Nome: "Cliente Teste",
                Email: "cliente@teste.com",
                Telefone: "11999999999",
                Senha: "Senha@123",
                Provedor: Provedor.Umio
            );

            var clienteMock = Cliente.CriarNovoCliente(input.Nome, input.Email, input.Telefone);
            var usuarioMock = Usuario.CriarNovoUsuario(input.Senha, clienteMock.Id, input.Provedor);

            _mockClienteRepository
                .Setup(repo => repo.CriarCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(true);

            _mockUsuarioRepository
                .Setup(repo => repo.CriarUsuario(It.IsAny<Usuario>()))
                .ReturnsAsync(true);

            // Act
            var resultado = await _criarCliente.Executar(input);

            // Assert
            resultado.Should().NotBeNull();
            resultado.Nome.Should().Be(input.Nome);
            resultado.Email.Should().Be(input.Email);
            resultado.Telefone.Should().Be(input.Telefone);

            _mockClienteRepository.Verify(repo => repo.CriarCliente(It.IsAny<Cliente>()), Times.Once);

            _mockUsuarioRepository.Verify(repo => repo.CriarUsuario(It.IsAny<Usuario>()), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoEmailJaExistir()
        {
            // Arrange
            var input = new CriarClienteInput(
                Nome: "Cliente Teste",
                Email: "cliente@teste.com",
                Telefone: "11999999999",
                Senha: "Senha@123",
                Provedor: Provedor.Umio
            );

            _mockClienteRepository
                .Setup(repo => repo.CriarCliente(It.IsAny<Cliente>()))
                .ThrowsAsync(new InvalidOperationException("Já existe um cliente com este e-mail."));

            // Act
            var act = async () => await _criarCliente.Executar(input);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Já existe um cliente com este e-mail.");

            _mockClienteRepository.Verify(repo => repo.CriarCliente(It.IsAny<Cliente>()), Times.Once);
            _mockUsuarioRepository.Verify(repo => repo.CriarUsuario(It.IsAny<Usuario>()), Times.Never);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoSenhaForInvalida()
        {
            // Arrange
            var input = new CriarClienteInput(
                Nome: "Cliente Teste",
                Email: "cliente@teste.com",
                Telefone: "11999999999",
                Senha: "123", // Senha inválida
                Provedor: Provedor.Umio
            );

            // Act
            var act = async () => await _criarCliente.Executar(input);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("A senha deve conter pelo menos 6 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais. (Parameter 'senha')");

            _mockClienteRepository.Verify(repo => repo.CriarCliente(It.IsAny<Cliente>()), Times.Never);
            _mockUsuarioRepository.Verify(repo => repo.CriarUsuario(It.IsAny<Usuario>()), Times.Never);
        }
    }
}