using FluentAssertions;
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
            endereco.Should().NotBeNull();
            endereco.Cep.Should().Be(cep);
            endereco.Rua.Should().Be(rua);
            endereco.Bairro.Should().Be(bairro);
            endereco.Cidade.Should().Be(cidade);
            endereco.UF.Should().Be(uf);
            endereco.Numero.Should().Be(numero);
            endereco.ClienteId.Should().Be(clienteId);
            endereco.Complemento.Should().Be(complemento);
            endereco.Ativo.Should().BeTrue();
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
            Action act = () => Endereco.CriarNovoEndereco(cep, rua, bairro, cidade, uf, numero, clienteId);
            act.Should().Throw<ExcecaoPropriedadeInvalida>();
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
            Action act = () => Endereco.CriarNovoEndereco(cep, rua, bairro, cidade, uf, numero, clienteId);
            act.Should().Throw<ExcecaoPropriedadeInvalida>();
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
            endereco.Should().NotBeNull();
            endereco.Cep.Should().Be(cep);
            endereco.Rua.Should().Be(rua);
            endereco.Bairro.Should().Be(bairro);
            endereco.Cidade.Should().Be(cidade);
            endereco.UF.Should().Be(uf);
        }

        [Fact]
        public void DesativarEndereco_DeveAlterarPropriedadeAtivoParaFalso()
        {
            // Arrange  
            var endereco = DadosEndereco.EnderecoValido;

            // Act  
            endereco.DesativarEndereco();

            // Assert  
            endereco.Ativo.Should().BeFalse();
        }
    }
}
