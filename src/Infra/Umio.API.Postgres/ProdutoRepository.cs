using Microsoft.EntityFrameworkCore;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades.Produtos;
using Umio.API.Postgres.Context;

namespace Umio.API.Postgres
{
    internal class ProdutoRepository : IProdutoRepository
    {
        private readonly UmioDbContext _context;
        public ProdutoRepository(UmioDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, Produto>> BuscarProdutosPorListaId(IEnumerable<int> listaId)
        {
            var produtos = await _context.Produtos
                .Where(p => listaId.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id, p => p);
            return produtos;
        }

        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();

            return produtos;
        }
    }
}
