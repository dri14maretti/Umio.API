using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Entities.Entidades
{
    [Table("cupom")]
    public class Cupom
    {
        private Cupom(string codigo, decimal porcentagemDesconto, bool ativo)
        {
            Codigo = codigo;
            PorcentagemDesconto = porcentagemDesconto;
            Ativo = ativo;
        }

        public static Cupom CriarCupom(string codigo, decimal porcentagemDesconto, bool ativo)
        {
            if (string.IsNullOrWhiteSpace(codigo))
                throw new ArgumentException("Código do cupom não pode ser vazio.", nameof(codigo));
            if (porcentagemDesconto < 0 || porcentagemDesconto > 100)
                throw new ArgumentOutOfRangeException(nameof(porcentagemDesconto), "Porcentagem de desconto deve estar entre 0 e 100.");

            return new Cupom(codigo, porcentagemDesconto, ativo);
        }

        [Key]
        [Column("codigo")]
        public string Codigo { get; private set; } = "";
        [Column("porcentagem")]
        public decimal PorcentagemDesconto { get; private set; }
        [Column("ativo")]
        public bool Ativo { get; private set; } // True = Ativo


        public decimal AplicarDesconto(decimal total)
        {
            if (!Ativo) throw new ExcecaoPropriedadeInvalida(Codigo);
            return total * (100 - PorcentagemDesconto) / 100;
        }

        public void Ativar()
        {
            Ativo = true;
        }
        public void Desativar() => Ativo = false;
    }
}