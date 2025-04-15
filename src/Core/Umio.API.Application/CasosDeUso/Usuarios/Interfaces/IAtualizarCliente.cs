using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Usuarios.Interfaces
{
    public interface IAtualizarCliente
    {
        Task<Cliente?> Execute(Guid id, string? nome, string? telefone, string? fotoUrl);
    }
}