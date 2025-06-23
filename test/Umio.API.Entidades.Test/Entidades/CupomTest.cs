using FluentAssertions;
using Umio.API.Entities.Entidades;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Entidades.Test.Entidades
{
    public class CupomTest
    {
        [Fact]
        public void AplicarDesconto_DeveRetornarValorComDesconto_QuandoCupomEstiverAtivo()
        {
            // Arrange
            var cupom = Cupom.CriarCupom("CUPOM123", 20m, true);
            var total = 100m;

            // Act
            var valorComDesconto = cupom.AplicarDesconto(total);

            // Assert
            valorComDesconto.Should().Be(80m); // 100 - 20% = 80
        }

        [Fact]
        public void AplicarDesconto_DeveLancarExcecao_QuandoCupomEstiverInativo()
        {
            // Arrange
            var cupom = Cupom.CriarCupom("CUPOM123", 20m, false);
            var total = 100m;

            // Act
            var act = () => cupom.AplicarDesconto(total);

            // Assert
            act.Should().Throw<ExcecaoPropriedadeInvalida>()
                .WithMessage($"A propriedade '{cupom.Codigo}' é inválida.");
        }

        [Fact]
        public void Ativar_DeveDefinirCupomComoAtivo()
        {
            // Arrange
            var cupom = Cupom.CriarCupom("CUPOM123", 20m, false);

            // Act
            cupom.Ativar();

            // Assert
            cupom.Ativo.Should().BeTrue();
        }

        [Fact]
        public void Desativar_DeveDefinirCupomComoInativo()
        {
            // Arrange
            var cupom = Cupom.CriarCupom("CUPOM123", 20m, true);

            // Act
            cupom.Desativar();

            // Assert
            cupom.Ativo.Should().BeFalse();
        }
    }
}