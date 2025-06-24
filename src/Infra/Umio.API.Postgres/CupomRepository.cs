using Microsoft.EntityFrameworkCore;
using Umio.API.Application.Contratos.Repositorios;
using Umio.API.Entities.Entidades;
using Umio.API.Postgres.Context;

namespace Umio.API.Postgres
{
    internal class CupomRepository : ICupomRepository
    {
        private readonly UmioDbContext _context;
        public CupomRepository(UmioDbContext context)
        {
            _context = context;
        }
        public async Task<Cupom> BuscarPorCodigo(string codigo, bool ativo)
        {
            var cupom = await _context.Cupoms
                .FirstOrDefaultAsync(c => c.Codigo == codigo && c.Ativo == ativo);

            return cupom;
        }
    }
}
