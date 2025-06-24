using FluentAssertions;
using Moq;
using Umio.API.Application.CasosDeUso.Clientes;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Test.CasosDeUso.Clientes
{
    public class ListarClienteTest
    {
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly ListarCliente _listarCliente;

        public ListarClienteTest()
        {
            _mockClienteRepository = new Mock<IClienteRepository>();
            _listarCliente = new ListarCliente(_mockClienteRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveRetornarClientes_QuandoFiltrosForemValidos()
        {
            // Arrange
            var clienteMock1 = Cliente.CriarNovoCliente("Cliente 1", "cliente1@exemplo.com", "11999999999");
            var clienteMock2 = Cliente.CriarNovoCliente("Cliente 2", "cliente2@exemplo.com", "11988888888");

            var clientesMock = new List<Cliente> { clienteMock1, clienteMock2 };

            _mockClienteRepository
                .Setup(repo => repo.ListarClientes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid?>()))
                .ReturnsAsync(clientesMock);

            var filtroNome = "Cliente";

            // Act
            var clientes = await _listarCliente.Executar(filtroNome);

            // Assert
            clientes.Should().NotBeNull();
            clientes.Should().HaveCount(2);
            clientes.Should().BeEquivalentTo(clientesMock);

            _mockClienteRepository.Verify(repo => repo.ListarClientes(filtroNome, null, null), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveRetornarListaVazia_QuandoNenhumClienteForEncontrado()
        {
            // Arrange
            _mockClienteRepository
                .Setup(repo => repo.ListarClientes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid?>()))
                .ReturnsAsync(new List<Cliente>());

            var filtroEmail = "naoexiste@exemplo.com";

            // Act
            var clientes = await _listarCliente.Executar(null, filtroEmail);

            // Assert
            clientes.Should().NotBeNull();
            clientes.Should().BeEmpty();

            _mockClienteRepository.Verify(repo => repo.ListarClientes(null, filtroEmail, null), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveRetornarClientePorId_QuandoIdForInformado()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var clienteMock = Cliente.ConstruirClientExistente(clienteId, "Cliente Único", "unico@exemplo.com", "11977777777");

            _mockClienteRepository
                .Setup(repo => repo.ListarClientes(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Guid?>()))
                .ReturnsAsync(new List<Cliente> { clienteMock });

            // Act
            var clientes = await _listarCliente.Executar(null, null, clienteId);

            // Assert
            clientes.Should().NotBeNull();
            clientes.Should().HaveCount(1);
            clientes.First().Id.Should().Be(clienteId);

            _mockClienteRepository.Verify(repo => repo.ListarClientes(null, null, clienteId), Times.Once);
        }
    }
}
