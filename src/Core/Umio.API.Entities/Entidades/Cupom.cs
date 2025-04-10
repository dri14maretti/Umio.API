using Umio.API.Entities.ObjetosDeValor;

namespace Umio.API.Entities.Entidades
{
    public class Cupom
    {
        public Guid Id { get; private set; }
        public string Codigo { get; private set; } = "";
        public decimal ValorDesconto { get; private set; }
        public DateTime DataValidade { get; private set; }
        public TipoDesconto TipoDesconto { get; private set; }
        public bool Status { get; private set; } // True = Ativo
        public int? UsosMaximos { get; private set; } // Null = ilimitado
        public int UsosAtuais { get; private set; }

        private Cupom() { }

        public Cupom(string codigo, decimal valorDesconto, DateTime dataValidade, TipoDesconto tipoDesconto, int? usosMaximos = null)
        {
            Id = Guid.NewGuid();
            Codigo = codigo.ToUpper();
            ValorDesconto = valorDesconto;
            DataValidade = dataValidade;
            TipoDesconto = tipoDesconto;
            UsosMaximos = usosMaximos;
            UsosAtuais = 0;
            Status = true; // Default deixa ativo

            Validar();
        }
        public bool EstaValido()
        {
            return Status &&
            DataValidade >= DateTime.UtcNow &&
            (UsosMaximos == null || UsosAtuais < UsosMaximos);
        }

        public decimal AplicarDesconto(decimal total)
    {
        return TipoDesconto == TipoDesconto.Porcentagem
            ? total * (1 - ValorDesconto) 
            : total - ValorDesconto;
    }

        public void Ativar()
        {
            Status = true;
            UsosAtuais = 0;
        }
        public void Desativar() => Status = false;

        public void AtualizarDesconto(decimal novoValor)
        {
            if (novoValor <= 0)
                throw new ArgumentException("Valor deve ser positivo");

            ValorDesconto = novoValor;
        }

        private void Validar()
        {
            if (ValorDesconto <= 0)
                throw new ArgumentException("Valor de desconto inválido");

            if (DataValidade < DateTime.UtcNow)
                throw new ArgumentException("Data de validade expirada");
        }
    }
}