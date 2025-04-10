using Umio.API.Entities.ObjetosDeValor;
namespace Umio.API.Entities.Entidades
{
    public abstract class Pagamento
    {
        public Guid Id { get; private set; }
        public Guid PedidoId { get; private set; }
        public decimal Valor { get; private set; }
        public DateTime DataPagamento { get; private set; }
        public StatusPagamento Status { get; private set; }
        public string? ComprovanteUrl { get; private set; }
        protected Pagamento(Guid id, Guid PedidoId, decimal valor, DateTime dataPagamento)
        {
            this.Id = id;
            this.PedidoId = PedidoId;
            Valor = valor;
            DataPagamento = DateTime.UtcNow; // Data de pagamento é sempre a data atual
            Status = StatusPagamento.Pendente;
            Validar();
        }

        public void AprovarPagamento(string comprovanteUrl)
        {
            if (Status != StatusPagamento.Pendente)
                throw new InvalidOperationException("Pagamento já processado");

            Status = StatusPagamento.Aprovado;
            ComprovanteUrl = comprovanteUrl;
        }

        public void CancelarPagamento()
        {
            if (Status == StatusPagamento.Aprovado)
                throw new InvalidOperationException("Pagamento já aprovado");

            Status = StatusPagamento.Cancelado;
        }

        protected virtual void Validar()
        {
            if (Valor <= 0)
                throw new ArgumentException("Valor deve ser positivo");
        }
        public abstract void Processar();
    }
}