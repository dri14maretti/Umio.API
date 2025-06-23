using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Umio.API.Entities.Exceptions;

namespace Umio.API.Entities.Entidades
{
    [Table("cupom")]
    public class Cupom
    {
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