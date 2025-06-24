using FluentAssertions;
using Moq;
using Umio.API.Application.CasosDeUso.Cupoms;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Application.Test.CasosDeUso.Cupoms
{
    public class BuscarCodigoCupomTests
    {
        private readonly Mock<ICupomRepository> _mockCupomRepository;
        private readonly BuscarCodigoCupom _buscarCodigoCupom;

        public BuscarCodigoCupomTests()
        {
            _mockCupomRepository = new Mock<ICupomRepository>();
            _buscarCodigoCupom = new BuscarCodigoCupom(_mockCupomRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveRetornarCupom_QuandoCupomExistirEEstiverAtivo()
        {
            // Arrange
            var codigo = "CUPOM123";
            var cupomEsperado = Cupom.CriarCupom
            (
                codigo,
                10m,
                true
            );

            _mockCupomRepository
                .Setup(repo => repo.BuscarPorCodigo(codigo, true))
                .ReturnsAsync(cupomEsperado);

            // Act
            var cupom = await _buscarCodigoCupom.Executar(codigo, true);

            // Assert
            cupom.Should().NotBeNull();
            cupom.Should().BeEquivalentTo(cupomEsperado);
            _mockCupomRepository.Verify(repo => repo.BuscarPorCodigo(codigo, true), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecaoElementoNaoEncontrado_QuandoCupomNaoExistir()
        {
            // Arrange
            var codigo = "CUPOM123";

            _mockCupomRepository
                .Setup(repo => repo.BuscarPorCodigo(codigo, true))
                .ReturnsAsync((Cupom)null);

            // Act
            var act = async () => await _buscarCodigoCupom.Executar(codigo, true);

            // Assert
            await act.Should().ThrowAsync<ExcecaoElementoNaoEncontrado>()
                .WithMessage($"O elemento 'cupom' com o identificador '{codigo}' não foi encontrado.");
            _mockCupomRepository.Verify(repo => repo.BuscarPorCodigo(codigo, true), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoRepositorioFalhar()
        {
            // Arrange
            var codigo = "CUPOM123";

            _mockCupomRepository
                .Setup(repo => repo.BuscarPorCodigo(codigo, true))
                .ThrowsAsync(new Exception("Erro ao acessar o repositório"));

            // Act
            var act = async () => await _buscarCodigoCupom.Executar(codigo, true);

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Erro ao acessar o repositório");
            _mockCupomRepository.Verify(repo => repo.BuscarPorCodigo(codigo, true), Times.Once);
        }
    }
}
