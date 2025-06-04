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
        public async Task<IEnumerable<Produto>> ListarProdutos()
        {
            var produtos = await _context.Produtos.ToListAsync();

            return produtos;
        }
    }
}
