using Microsoft.EntityFrameworkCore;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Postgres.Context;

namespace Umio.API.Postgres
{
    class CategoriaProdutoRepository : ICategoriaProdutoRepository
    {
        private readonly UmioDbContext _context;
        public CategoriaProdutoRepository(UmioDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoriaProduto>> ListarTodas()
        {
            var categoriasProduto = await _context.CategoriaProduto.ToListAsync();

            return categoriasProduto;
        }
    }
}
