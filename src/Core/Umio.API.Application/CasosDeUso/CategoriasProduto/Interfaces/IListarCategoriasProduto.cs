using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.CategoriasProduto.Interfaces
{
    public interface IListarCategoriasProduto
    {
        Task<IEnumerable<CategoriaProduto>> Executar();
    }
}
