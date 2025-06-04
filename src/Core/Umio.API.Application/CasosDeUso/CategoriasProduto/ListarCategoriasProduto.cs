using Umio.API.Application.CasosDeUso.CategoriasProduto.Interfaces;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.CategoriasProduto
{
    public class ListarCategoriasProduto : IListarCategoriasProduto
    {
        private readonly ICategoriaProdutoRepository _categoriaProdutoRepository;
        public ListarCategoriasProduto(ICategoriaProdutoRepository categoriaProdutoRepository)
        {
            _categoriaProdutoRepository = categoriaProdutoRepository;
        }
        public async Task<IEnumerable<CategoriaProduto>> Executar() =>
            await _categoriaProdutoRepository.ListarTodas();
    }
}
