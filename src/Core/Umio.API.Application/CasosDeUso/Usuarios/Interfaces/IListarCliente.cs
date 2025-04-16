using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios.Interfaces
{
    public interface IListarCliente
    {
        Task<IEnumerable<Cliente>> Executar(string? nome = null, string? email = null, string? telefone = null);
    }
}