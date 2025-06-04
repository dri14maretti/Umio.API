using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface ICategoriaProdutoRepository
    {
        public Task<IEnumerable<CategoriaProduto>> ListarTodas();
    }
}
