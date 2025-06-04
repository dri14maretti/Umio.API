using Umio.API.Application.CasosDeUso.Produtos.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades.Produtos;

namespace Umio.API.Application.CasosDeUso.Produtos
{
    public class ListarProdutos : IListarProdutos
    {
        private readonly IProdutoRepository _produtoRepository;
        public ListarProdutos(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }
        public async Task<IEnumerable<Produto>> Executar()
        {
            return await _produtoRepository.ListarProdutos();
        }
    }
}
