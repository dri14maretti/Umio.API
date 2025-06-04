using FluentAssertions;
using Moq;
using Umio.API.Application.CasosDeUso.CategoriasProduto;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.TestData.Entidades;

namespace Umio.API.Application.Test.CasosDeUso.Categorias
{
    public class ListarCategoriasProdutoTests
    {
        private readonly Mock<ICategoriaProdutoRepository> _mockCategoriaProdutoRepository;
        private readonly ListarCategoriasProduto _listarCategoriasProduto;

        public ListarCategoriasProdutoTests()
        {
            _mockCategoriaProdutoRepository = new Mock<ICategoriaProdutoRepository>();
            _listarCategoriasProduto = new ListarCategoriasProduto(_mockCategoriaProdutoRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveRetornarListaDeCategorias_QuandoCategoriasExistirem()
        {
            // Arrange
            var categoriasEsperadas = DadosCategoriaProduto.ListaCategorias;

            _mockCategoriaProdutoRepository
                .Setup(repo => repo.ListarTodas())
                .ReturnsAsync(categoriasEsperadas);

            // Act
            var categorias = await _listarCategoriasProduto.Executar();

            // Assert
            categorias.Should().NotBeNull();
            categorias.Should().HaveCount(2);
            categorias.Should().BeEquivalentTo(categoriasEsperadas);
            _mockCategoriaProdutoRepository.Verify(repo => repo.ListarTodas(), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveRetornarListaVazia_QuandoNenhumaCategoriaExistir()
        {
            // Arrange
            _mockCategoriaProdutoRepository
                .Setup(repo => repo.ListarTodas())
                .ReturnsAsync(new List<CategoriaProduto>());

            // Act
            var categorias = await _listarCategoriasProduto.Executar();

            // Assert
            categorias.Should().NotBeNull();
            categorias.Should().BeEmpty();
            _mockCategoriaProdutoRepository.Verify(repo => repo.ListarTodas(), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoRepositorioFalhar()
        {
            // Arrange
            _mockCategoriaProdutoRepository
                .Setup(repo => repo.ListarTodas())
                .ThrowsAsync(new Exception("Erro ao acessar o repositório"));

            // Act
            var act = async () => await _listarCategoriasProduto.Executar();

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Erro ao acessar o repositório");
            _mockCategoriaProdutoRepository.Verify(repo => repo.ListarTodas(), Times.Once);
        }
    }
}
