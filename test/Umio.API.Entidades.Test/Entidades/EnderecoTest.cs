using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;
using Umio.API.TestData.Entidades;

namespace Umio.API.Entidades.Test.Entidades
{
    public class EnderecoTests
    {
        [Fact]
        public void CriarNovoEndereco_DeveCriarEnderecoComDadosValidos()
        {
            // Arrange
            var cep = DadosEndereco.Cep;
            var rua = DadosEndereco.Rua;
            var bairro = DadosEndereco.Bairro;
            var cidade = DadosEndereco.Cidade;
            var uf = DadosEndereco.UF;
            var numero = DadosEndereco.Numero;
            var clienteId = DadosEndereco.ClienteId;
            var complemento = DadosEndereco.Complemento;

            // Act
            var endereco = Endereco.CriarNovoEndereco(cep, rua, bairro, cidade, uf, numero, clienteId, complemento);

            // Assert
            Assert.NotNull(endereco);
            Assert.Equal(cep, endereco.Cep);
            Assert.Equal(rua, endereco.Rua);
            Assert.Equal(bairro, endereco.Bairro);
            Assert.Equal(cidade, endereco.Cidade);
            Assert.Equal(uf, endereco.UF);
            Assert.Equal(numero, endereco.Numero);
            Assert.Equal(clienteId, endereco.ClienteId);
            Assert.Equal(complemento, endereco.Complemento);
            Assert.True(endereco.Ativo);
        }

        [Theory]
        [InlineData("")]
        [InlineData("123")]
        [InlineData(null)]
        public void CriarNovoEndereco_DeveLancarExcecaoParaCepInvalido(string cep)
        {
            // Arrange
            var rua = DadosEndereco.Rua;
            var bairro = DadosEndereco.Bairro;
            var cidade = DadosEndereco.Cidade;
            var uf = DadosEndereco.UF;
            var numero = DadosEndereco.Numero;
            var clienteId = DadosEndereco.ClienteId;

            // Act & Assert
            Assert.Throws<ExcecaoPropriedadeInvalida>(() => Endereco.CriarNovoEndereco(cep, rua, bairro, cidade, uf, numero, clienteId));
        }

        [Fact]
        public void CriarNovoEndereco_DeveLancarExcecaoParaNumeroInvalido()
        {
            // Arrange
            var cep = DadosEndereco.Cep;
            var rua = DadosEndereco.Rua;
            var bairro = DadosEndereco.Bairro;
            var cidade = DadosEndereco.Cidade;
            var uf = DadosEndereco.UF;
            var numero = 0; // Número inválido
            var clienteId = DadosEndereco.ClienteId;

            // Act & Assert
            Assert.Throws<ExcecaoPropriedadeInvalida>(() => Endereco.CriarNovoEndereco(cep, rua, bairro, cidade, uf, numero, clienteId));
        }

        [Fact]
        public void CriarEnderecoSemNumero_DeveCriarEnderecoComDadosValidos()
        {
            // Arrange
            var cep = DadosEndereco.Cep;
            var rua = DadosEndereco.Rua;
            var bairro = DadosEndereco.Bairro;
            var cidade = DadosEndereco.Cidade;
            var uf = DadosEndereco.UF;

            // Act
            var endereco = Endereco.CriarEnderecoSemNumero(cep, rua, bairro, cidade, uf);

            // Assert
            Assert.NotNull(endereco);
            Assert.Equal(cep, endereco.Cep);
            Assert.Equal(rua, endereco.Rua);
            Assert.Equal(bairro, endereco.Bairro);
            Assert.Equal(cidade, endereco.Cidade);
            Assert.Equal(uf, endereco.UF);
        }

        [Fact]
        public void DesativarEndereco_DeveAlterarPropriedadeAtivoParaFalso()
        {
            // Arrange
            var endereco = DadosEndereco.EnderecoValido;

            // Act
            endereco.DesativarEndereco();

            // Assert
            Assert.False(endereco.Ativo);
        }
    }
}
