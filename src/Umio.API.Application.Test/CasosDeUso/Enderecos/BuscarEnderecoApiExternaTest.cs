using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umio.API.Application.CasosDeUso.Enderecos;
using Umio.API.Application.CasosDeUso.Enderecos.Interfaces;
using Umio.API.Application.Contratos.Servicos;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Test.CasosDeUso.Enderecos
{
    public class BuscarEnderecoApiExternaTest
    {
        private readonly Mock<ICepService> _mockCepService;
        private readonly BuscarEnderecoApiExterna _buscarEnderecoApiExterna;

        public BuscarEnderecoApiExternaTest()
        {
            _mockCepService = new Mock<ICepService>();
            _buscarEnderecoApiExterna = new BuscarEnderecoApiExterna(_mockCepService.Object);
        }

        [Fact]
        public async Task Executar_DeveRetornarEndereco_QuandoCepForValido()
        {
            // Arrange
            var cep = "12345-678";
            var enderecoEsperado = Endereco.CriarEnderecoSemNumero("37500-202", "Rua teste", "Teste", "Itajubar", "MG");

            _mockCepService
                .Setup(service => service.BuscarEnderecoPorCep(cep))
                .ReturnsAsync(enderecoEsperado);

            // Act
            var endereco = await _buscarEnderecoApiExterna.Executar(cep);

            // Assert
            endereco.Should().NotBeNull();
            endereco.Should().BeEquivalentTo(enderecoEsperado);
            _mockCepService.Verify(service => service.BuscarEnderecoPorCep(cep), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveRetornarNull_QuandoCepNaoExistir()
        {
            // Arrange
            var cep = "00000-000";

            _mockCepService
                .Setup(service => service.BuscarEnderecoPorCep(cep))
                .ReturnsAsync((Endereco)null);

            // Act
            var endereco = await _buscarEnderecoApiExterna.Executar(cep);

            // Assert
            endereco.Should().BeNull();
            _mockCepService.Verify(service => service.BuscarEnderecoPorCep(cep), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoServicoFalhar()
        {
            // Arrange
            var cep = "12345-678";

            _mockCepService
                .Setup(service => service.BuscarEnderecoPorCep(cep))
                .ThrowsAsync(new Exception("Erro ao acessar o serviço"));

            // Act
            var act = async () => await _buscarEnderecoApiExterna.Executar(cep);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Erro ao acessar o serviço");
            _mockCepService.Verify(service => service.BuscarEnderecoPorCep(cep), Times.Once);
        }
    }
}
