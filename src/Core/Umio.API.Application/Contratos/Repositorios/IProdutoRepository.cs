using Umio.API.Entities.Entidades.Produtos;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> ListarProdutos();
        Task<Dictionary<int, Produto>> BuscarProdutosPorListaId(IEnumerable<int> listaId);
    }
}
