using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios.Interfaces
{
    public interface IAtualizarCliente
    {
        Task<Cliente?> Executar(Guid id, string? nome, string? telefone);
    }
}