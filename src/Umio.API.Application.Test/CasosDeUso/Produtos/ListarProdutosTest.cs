using FluentAssertions;
using Moq;
using Umio.API.Application.CasosDeUso.Produtos;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades.Produtos;
using Umio.API.TestData.Entidades;

namespace Umio.API.Application.Test.CasosDeUso.Produtos
{
    public class ListarProdutosTests
    {
        private readonly Mock<IProdutoRepository> _mockProdutoRepository;
        private readonly ListarProdutos _listarProdutos;

        public ListarProdutosTests()
        {
            _mockProdutoRepository = new Mock<IProdutoRepository>();
            _listarProdutos = new ListarProdutos(_mockProdutoRepository.Object);
        }

        [Fact]
        public async Task Executar_DeveRetornarListaDeProdutos_QuandoProdutosExistirem()
        {
            // Arrange
            var produtosEsperados = DadosProduto.ListaProdutos;

            _mockProdutoRepository
                .Setup(repo => repo.ListarProdutos())
                .ReturnsAsync(produtosEsperados);

            // Act
            var produtos = await _listarProdutos.Executar();

            // Assert
            produtos.Should().NotBeNull();
            produtos.Should().HaveCount(2);
            produtos.Should().BeEquivalentTo(produtosEsperados);
            _mockProdutoRepository.Verify(repo => repo.ListarProdutos(), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveRetornarListaVazia_QuandoNenhumProdutoExistir()
        {
            // Arrange
            _mockProdutoRepository
                .Setup(repo => repo.ListarProdutos())
                .ReturnsAsync(new List<Produto>());

            // Act
            var produtos = await _listarProdutos.Executar();

            // Assert
            produtos.Should().NotBeNull();
            produtos.Should().BeEmpty();
            _mockProdutoRepository.Verify(repo => repo.ListarProdutos(), Times.Once);
        }

        [Fact]
        public async Task Executar_DeveLancarExcecao_QuandoRepositorioFalhar()
        {
            // Arrange
            _mockProdutoRepository
                .Setup(repo => repo.ListarProdutos())
                .ThrowsAsync(new Exception("Erro ao acessar o repositório"));

            // Act
            var act = async () => await _listarProdutos.Executar();

            // Assert
            await act.Should().ThrowAsync<Exception>().WithMessage("Erro ao acessar o repositório");
            _mockProdutoRepository.Verify(repo => repo.ListarProdutos(), Times.Once);
        }
    }
}
