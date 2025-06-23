using Umio.API.Entities.Entidades;

namespace Umio.API.Application.Contratos.Repositorios
{
    public interface ICupomRepository
    {
        public Task<Cupom> BuscarPorCodigo(string codigo, bool ativo);
    }
}
