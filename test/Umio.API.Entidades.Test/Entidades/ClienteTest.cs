using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;
using Umio.API.TestData.Entidades;

namespace Umio.API.Entidades.Test.Entidades
{
    public class ClienteTest
    {
        private readonly DadosCliente _dadosCliente;
        public ClienteTest()
        {
            _dadosCliente = new();
        }
        [Fact]
        public void CriarNovoCliente_DeveCriarClienteComDadosValidos()
        {
            // Arrange
            var nome = DadosCliente.Nome;
            var email = DadosCliente.Email;
            var telefone = DadosCliente.Telefone;

            // Act
            var cliente = Cliente.CriarNovoCliente(nome, email, telefone);

            // Assert
            Assert.NotNull(cliente);
            Assert.Equal(nome, cliente.Nome);
            Assert.Equal(email, cliente.Email);
            Assert.Equal(0, cliente.Pontos);
        }

        [Theory]
        [InlineData("")]
        [InlineData("email_invalido")]
        [InlineData(null)]
        public void CriarNovoCliente_DeveLancarExcecaoParaEmailInvalido(string email)
        {
            // Arrange
            var nome = DadosCliente.Nome;
            var telefone = DadosCliente.Telefone;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Cliente.CriarNovoCliente(nome, email, telefone));
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("")]
        public void CriarNovoCliente_DeveLancarExcecaoParaTelefoneInvalido(string telefone)
        {
            // Arrange
            var nome = DadosCliente.Nome;
            var email = DadosCliente.Email;

            // Act & Assert
            Assert.Throws<ExcecaoPropriedadeInvalida>(() => Cliente.CriarNovoCliente(nome, email, telefone));
        }

        [Fact]
        public void AdicionarPontos_DeveAdicionarPontosValidos()
        {
            // Arrange
            var cliente = _dadosCliente.ClienteValido;

            // Act
            cliente.AdicionarPontos(10);

            // Assert
            Assert.Equal(10, cliente.Pontos);
        }

        [Fact]
        public void AdicionarPontos_DeveLancarExcecaoParaPontosInvalidos()
        {
            // Arrange
            var cliente = _dadosCliente.ClienteValido;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => cliente.AdicionarPontos(0));
            Assert.Throws<ArgumentException>(() => cliente.AdicionarPontos(-5));
        }

        [Fact]
        public void AtualizarCliente_DeveAtualizarDadosValidos()
        {
            // Arrange
            var cliente = _dadosCliente.ClienteValido;

            // Act
            cliente.AtualizarCliente(nome: "João Atualizado", telefone: "11912345678", email: "joao.atualizado@email.com", pontos: 50);

            // Assert
            Assert.Equal("João Atualizado", cliente.Nome);
            Assert.Equal("11912345678", cliente.Telefone);
            Assert.Equal("joao.atualizado@email.com", cliente.Email);
            Assert.Equal(50, cliente.Pontos);
        }
    }
}
