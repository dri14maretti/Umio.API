using FluentAssertions;
using Moq;
using Umio.API.Application.CasosDeUso.Clientes;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Test.CasosDeUso.Clientes
{
    public class AtualizarClienteTest
    {
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly AtualizarCliente _atualizarCliente;

        public AtualizarClienteTest()
        {
            _mockClienteRepository = new Mock<IClienteRepository>();
            _atualizarCliente = new AtualizarCliente(_mockClienteRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveAtualizarCliente_QuandoDadosForemValidos()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var clienteMock = Cliente.CriarNovoCliente("Cliente Original", "original@exemplo.com", "11999999999");

            var novoNome = "Cliente Atualizado";
            var novoEmail = "atualizado@exemplo.com";
            var novoTelefone = "11988888888";
            var novosPontos = 100;

            var clienteAtualizadoMock = Cliente.CriarNovoCliente(novoNome, novoEmail, novoTelefone);

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync(clienteMock);

            _mockClienteRepository
                .Setup(repo => repo.AtualizarCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(clienteAtualizadoMock);


            // Act
            var clienteAtualizado = await _atualizarCliente.Executar(clienteId, novoNome, novoTelefone, novoEmail, novosPontos);

            // Assert
            clienteAtualizado.Should().NotBeNull();
            clienteAtualizado.Nome.Should().Be(novoNome);
            clienteAtualizado.Email.Should().Be(novoEmail);
            clienteAtualizado.Telefone.Should().Be(novoTelefone);
            clienteAtualizado.Pontos.Should().Be(novosPontos);

            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockClienteRepository.Verify(repo => repo.AtualizarCliente(It.IsAny<Cliente>()), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveRetornarNull_QuandoClienteNaoExistir()
        {
            // Arrange
            var clienteId = Guid.NewGuid();

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync((Cliente)null);

            // Act
            var clienteAtualizado = await _atualizarCliente.Executar(clienteId, "Novo Nome", "11988888888", "novo@exemplo.com", 50);

            // Assert
            clienteAtualizado.Should().BeNull();
            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockClienteRepository.Verify(repo => repo.AtualizarCliente(It.IsAny<Cliente>()), Times.Never);
        }

        [Fact]
        public async Task Executar_DeveAtualizarSomenteCamposInformados()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var clienteMock = Cliente.CriarNovoCliente("Cliente Original", "original@exemplo.com", "11999999999");

            var clienteAtualizadoMock = Cliente.CriarNovoCliente("Cliente Atualizado", "original@exemplo.com", "11999999999");

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync(clienteMock);

            _mockClienteRepository
                .Setup(repo => repo.AtualizarCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(clienteAtualizadoMock);

            var novoNome = "Cliente Atualizado";

            // Act
            var clienteAtualizado = await _atualizarCliente.Executar(clienteId, novoNome, null, null, null);

            // Assert
            clienteAtualizado.Should().NotBeNull();
            clienteAtualizado.Nome.Should().Be(novoNome);
            clienteAtualizado.Email.Should().Be(clienteMock.Email);
            clienteAtualizado.Telefone.Should().Be(clienteMock.Telefone);
            clienteAtualizado.Pontos.Should().Be(clienteMock.Pontos);

            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockClienteRepository.Verify(repo => repo.AtualizarCliente(It.IsAny<Cliente>()), Times.Once);
        }
    }
}
