using Umio.API.Entities.ObjetosDeValor;
using Umio.API.Entities.Entidades.Produtos;

namespace Umio.API.Entities.Entidades
{
    public class Pedido
    {

        public Guid Id { get; private set; }
        public Guid ClienteId { get; private set; }
        public Guid EnderecoId { get; private set; }
        public List<ItemPedido> Itens { get; private set; } = [];
        public string Comentarios { get; private set; } = "";
        public decimal ValorEntrega { get; private set; }
        public decimal Total => CalcularTotal();
        public StatusPedido Status { get; private set; }
        public Cupom? Cupom { get; private set; }
        public DateTime DataPedido { get; private set; }
        public Guid? PagamentoId { get; private set; }
        public Pagamento? Pagamento { get; private set; }

        private Pedido()
        {
            Itens = [];
        }

        public Pedido(Guid clienteId, Guid enderecoId, string comentarios, decimal valorEntrega, Cupom? cupom = null)
        {
            Id = Guid.NewGuid();
            ClienteId = clienteId;
            EnderecoId = enderecoId;
            Comentarios = comentarios;
            ValorEntrega = valorEntrega;
            Cupom = cupom;
            Status = StatusPedido.Pendente;
            DataPedido = DateTime.UtcNow;
            Itens = [];
            Validar();
        }

        private decimal CalcularTotal()
        {
            decimal subtotalItens = Itens.Sum(item =>
                (item.Produto?.Preco ?? 0) * item.Quantidade);
            decimal total = subtotalItens + ValorEntrega;

            if (Cupom?.EstaValido() == true)
            {
                total -= Cupom.ValorDesconto;
            }

            return total;
        }

        public void AdicionarItem(ItemPedido item)
        {
            if (Status != StatusPedido.Pendente)
                throw new InvalidOperationException("Pedido já processado");

            Itens.Add(item);
        }

        public void AtualizarStatus(StatusPedido novoStatus)
        {
            if (novoStatus == StatusPedido.Pendente && Status != StatusPedido.Pendente)
                throw new InvalidOperationException("Transição de status inválida");

            Status = novoStatus;
        }

        private void Validar()
        {
            if (ValorEntrega < 0)
                throw new ArgumentException("Valor de entrega inválido");
        }

        public void AplicarCupom(Cupom cupom)
        {
            if (Status != StatusPedido.Pendente)
                throw new InvalidOperationException("Pedido já processado");

            if (!cupom.EstaValido())
                throw new InvalidOperationException("Cupom inválido");

            Cupom = cupom;
            cupom.AplicarDesconto(Total);
        }

        public void AdicionarPagamento(Pagamento pagamento)
        {
            if (Status != StatusPedido.Pendente)
                throw new InvalidOperationException("Pedido já processado");

            if (pagamento.Valor != Total)
                throw new ArgumentException("Valor do pagamento incompatível");

            Pagamento = pagamento;
            Pagamento.Processar();
        }
    }
}