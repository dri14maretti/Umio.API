using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using System.Reflection.Metadata;
using Umio.API.Application.CasosDeUso.Enderecos;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Application.Test.CasosDeUso.Enderecos
{
    public class BuscarEnderecosClienteTest
    {
        private readonly Mock<IEnderecoRepository> _mockEnderecoRepository;
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly BuscarEnderecosCliente _buscarEnderecosCliente;

        public BuscarEnderecosClienteTest()
        {
            _mockEnderecoRepository = new Mock<IEnderecoRepository>();
            _mockClienteRepository = new Mock<IClienteRepository>();
            _buscarEnderecosCliente = new BuscarEnderecosCliente(_mockEnderecoRepository.Object, _mockClienteRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveRetornarEnderecos_QuandoClienteForValido()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var clienteMock = Cliente.CriarNovoCliente("Cliente Exemplo", "cliente@exemplo.com", "11999999999");

            var enderecosMock = new List<Endereco>
            {
                Endereco.CriarNovoEndereco("12345678", "Rua A", "Bairro A", "Cidade A", "SP", 123, clienteId),
                Endereco.CriarNovoEndereco("87654321", "Rua B", "Bairro B", "Cidade B", "RJ", 456, clienteId)
            };

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync(clienteMock);

            _mockEnderecoRepository
                .Setup(repo => repo.BuscarEnderecosCliente(clienteId))
                .ReturnsAsync(enderecosMock);

            // Act
            var enderecos = await _buscarEnderecosCliente.Executar(clienteId);

            // Assert
            enderecos.Should().NotBeNull();
            enderecos.Should().HaveCount(2);
            enderecos.Should().BeEquivalentTo(enderecosMock);
            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockEnderecoRepository.Verify(repo => repo.BuscarEnderecosCliente(clienteId), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoClienteNaoExistir()
        {
            // Arrange
            var clienteId = Guid.NewGuid();

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync((Cliente)null);

            // Act
            var act = async () => await _buscarEnderecosCliente.Executar(clienteId);

            // Assert
            await act.Should().ThrowAsync<ExcecaoParametroIncorreto>()
                .WithMessage($"O parametro '{clienteId}' é inválido. Não é possível buscar endereços de um cliente inválido");
            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockEnderecoRepository.Verify(repo => repo.BuscarEnderecosCliente(It.IsAny<Guid>()), Times.Never);
        }

        [Fact]
        public async Task Executar_DeveRetornarListaVazia_QuandoClienteNaoPossuirEnderecos()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var clienteMock = Cliente.CriarNovoCliente("Cliente Exemplo", "cliente@exemplo.com", "11999999999");

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync(clienteMock);

            _mockEnderecoRepository
                .Setup(repo => repo.BuscarEnderecosCliente(clienteId))
                .ReturnsAsync(new List<Endereco>());

            // Act
            var enderecos = await _buscarEnderecosCliente.Executar(clienteId);

            // Assert
            enderecos.Should().NotBeNull();
            enderecos.Should().BeEmpty();
            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockEnderecoRepository.Verify(repo => repo.BuscarEnderecosCliente(clienteId), Times.Once);
        }
    }
}
