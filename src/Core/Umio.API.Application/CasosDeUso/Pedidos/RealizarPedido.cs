using Umio.API.Application.CasosDeUso.Pedidos.Interfaces;
using Umio.API.Application.CasosDeUso.Pedidos.Model;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Pedidos
{
    internal class RealizarPedido : IRealizarPedido
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ICupomRepository _cupomRepository;

        public RealizarPedido(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IEnderecoRepository enderecoRepository, ICupomRepository cupomRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
            _enderecoRepository = enderecoRepository;
            _cupomRepository = cupomRepository;
        }

        public async Task<Pedido> Executar(PedidoModel pedidoModel, Guid clienteId)
        {
            var produtoIds = pedidoModel.Itens.Select(item => item.ProdutoId).ToList();
            var endereco = await _enderecoRepository.BuscarEnderecoPorId(pedidoModel.EnderecoId);
            var cupom = await _cupomRepository.BuscarPorCodigo(pedidoModel.CodigoCupom, true);

            return null;
        }
    }
}
