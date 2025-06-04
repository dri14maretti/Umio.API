using Umio.API.Entities.Entidades;
using Umio.API.Entities.Entidades.Enums;
using Umio.API.TestData.Entidades;

namespace Umio.API.Entidades.Test.Entidades
{
    public class UsuarioTests
    {
        [Fact]
        public void CriarNovoUsuario_DeveCriarUsuarioComDadosValidos()
        {
            // Arrange
            var usuarioEsperado = DadosUsuario.UsuarioValido;

            // Act
            var usuario = Usuario.CriarNovoUsuario(DadosUsuario.Senha, usuarioEsperado.ClienteId, DadosUsuario.Provedor);

            // Assert
            Assert.NotNull(usuario);
            Assert.Equal(usuarioEsperado.ClienteId, usuario.ClienteId);
            Assert.Equal(usuarioEsperado.Provedor, usuario.Provedor);
            Assert.NotEqual(DadosUsuario.Senha, usuario.Senha); // A senha deve estar criptografada
        }

        [Theory]
        [InlineData("")]
        [InlineData("12345")]
        [InlineData("senha123")]
        [InlineData("SENHA123")]
        [InlineData("Senha")]
        public void CriarNovoUsuario_DeveLancarExcecaoParaSenhaFraca(string senha)
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => Usuario.CriarNovoUsuario(senha, DadosUsuario.ClienteId, DadosUsuario.Provedor));
            Assert.Contains("A senha deve conter pelo menos 6 caracteres", exception.Message);
        }

        [Fact]
        public void ValidarSenha_DeveRetornarTrueParaSenhaCorreta()
        {
            // Arrange
            var usuario = DadosUsuario.UsuarioValido;

            // Act
            var resultado = usuario.ValidarSenha(DadosUsuario.Senha);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void ValidarSenha_DeveRetornarFalseParaSenhaIncorreta()
        {
            // Arrange
            var usuario = DadosUsuario.UsuarioValido;

            // Act
            var resultado = usuario.ValidarSenha("SenhaErrada@123");

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public void ValidarSenha_DeveRetornarFalseParaSenhaVazia()
        {
            // Arrange
            var usuario = DadosUsuario.UsuarioValido;

            // Act
            var resultado = usuario.ValidarSenha("");

            // Assert
            Assert.False(resultado);
        }
    }
}
