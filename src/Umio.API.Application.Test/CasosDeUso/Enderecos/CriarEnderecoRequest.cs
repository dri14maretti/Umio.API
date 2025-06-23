using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umio.API.Application.CasosDeUso.Enderecos;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Application.Contratos;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Application.Test.CasosDeUso.Enderecos
{
    public class CriarEnderecoTest
    {
        private readonly Mock<IEnderecoRepository> _mockEnderecoRepository;
        private readonly Mock<IClienteRepository> _mockClienteRepository;
        private readonly CriarEndereco _criarEndereco;

        public CriarEnderecoTest()
        {
            _mockEnderecoRepository = new Mock<IEnderecoRepository>();
            _mockClienteRepository = new Mock<IClienteRepository>();
            _criarEndereco = new CriarEndereco(_mockEnderecoRepository.Object, _mockClienteRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveCriarEndereco_QuandoClienteForValido()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var clienteMock = Cliente.CriarNovoCliente("Cliente Exemplo", "cliente@exemplo.com", "11999999999");

            var request = new CriarEnderecoRequest
            {
                Cep = "12345678",
                Rua = "Rua A",
                Bairro = "Bairro A",
                Cidade = "Cidade A",
                Estado = "SP",
                Numero = 123,
                Complemento = "Apto 101"
            };

            var enderecoMock = Endereco.CriarNovoEndereco(
                request.Cep, request.Rua, request.Bairro, request.Cidade, request.Estado, request.Numero, clienteId, request.Complemento);

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync(clienteMock);

            _mockEnderecoRepository
                .Setup(repo => repo.CriarEndereco(It.IsAny<Endereco>()))
                .ReturnsAsync(true);

            // Act
            var resultado = await _criarEndereco.Executar(request, clienteId);

            // Assert
            resultado.Should().BeTrue();
            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockEnderecoRepository.Verify(repo => repo.CriarEndereco(It.Is<Endereco>(e =>
                e.Cep == request.Cep &&
                e.Rua == request.Rua &&
                e.Bairro == request.Bairro &&
                e.Cidade == request.Cidade &&
                e.UF == request.Estado &&
                e.Numero == request.Numero &&
                e.Complemento == request.Complemento &&
                e.ClienteId == clienteId)), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoClienteNaoExistir()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var request = new CriarEnderecoRequest
            {
                Cep = "12345678",
                Rua = "Rua A",
                Bairro = "Bairro A",
                Cidade = "Cidade A",
                Estado = "SP",
                Numero = 123,
                Complemento = "Apto 101"
            };

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync((Cliente)null);

            // Act
            var act = async () => await _criarEndereco.Executar(request, clienteId);

            // Assert
            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Não é possível criar um endereço sem um usuário válido atrelado");
            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockEnderecoRepository.Verify(repo => repo.CriarEndereco(It.IsAny<Endereco>()), Times.Never);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoDadosDoEnderecoForemInvalidos()
        {
            // Arrange
            var clienteId = Guid.NewGuid();
            var clienteMock = Cliente.CriarNovoCliente("Cliente Exemplo", "cliente@exemplo.com", "11999999999");

            var request = new CriarEnderecoRequest
            {
                Cep = "123", // CEP inválido
                Rua = "Rua A",
                Bairro = "Bairro A",
                Cidade = "Cidade A",
                Estado = "SP",
                Numero = 123,
                Complemento = "Apto 101"
            };

            _mockClienteRepository
                .Setup(repo => repo.BuscarClientePorId(clienteId))
                .ReturnsAsync(clienteMock);

            // Act
            var act = async () => await _criarEndereco.Executar(request, clienteId);

            // Assert
            await act.Should().ThrowAsync<ExcecaoPropriedadeInvalida>()
                .WithMessage("A propriedade 'cep' é inválida. CEP inválido.");
            _mockClienteRepository.Verify(repo => repo.BuscarClientePorId(clienteId), Times.Once);
            _mockEnderecoRepository.Verify(repo => repo.CriarEndereco(It.IsAny<Endereco>()), Times.Never);
        }
    }
}
