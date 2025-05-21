using Umio.API.Entities.Entidades.Produtos;

namespace Umio.API.Application.CasosDeUso.Produtos.Interfaces
{
    public interface IListarProdutos
    {
        Task<IEnumerable<Produto>> Executar();
    }
}
