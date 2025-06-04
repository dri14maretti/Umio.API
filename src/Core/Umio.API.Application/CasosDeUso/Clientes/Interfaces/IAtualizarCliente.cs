using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Clientes.Interfaces
{
    public interface IAtualizarCliente
    {
        Task<Cliente?> Executar(Guid id, string? nome, string? telefone, string? email, int? pontos);
    }
}