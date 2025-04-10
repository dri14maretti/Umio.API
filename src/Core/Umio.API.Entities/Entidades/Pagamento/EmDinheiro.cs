namespace Umio.API.Entities.Entidades
{
    public class Dinheiro : Pagamento
    {
        public Dinheiro(Guid id, Guid clienteId, decimal valor, decimal? trocoPara = null): base(id, clienteId, valor, DateTime.UtcNow)
        {
            TrocoPara = trocoPara;
            Validar();
        }

        public decimal? TrocoPara { get; private set; }

        public override void Processar()
        {
            if (TrocoPara.HasValue && TrocoPara < Valor)
                throw new InvalidOperationException("Valor insuficiente");

            base.AprovarPagamento(null); // Sem comprovante para dinheiro
        }

        protected override void Validar()
        {
            base.Validar();
            if (TrocoPara.HasValue && TrocoPara <= 0)
                throw new ArgumentException("Troco deve ser positivo");
        }
    }
}