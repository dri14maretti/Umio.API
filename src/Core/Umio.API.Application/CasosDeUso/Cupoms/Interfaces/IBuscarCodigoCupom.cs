using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Cupoms.Interfaces
{
    public interface IBuscarCodigoCupom
    {
        Task<Cupom> Executar(string codigo, bool ativo);
    }
}
