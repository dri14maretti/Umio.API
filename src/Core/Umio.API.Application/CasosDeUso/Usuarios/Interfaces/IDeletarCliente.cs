namespace Umio.API.Application.CasosDeUso.Usuarios.Interfaces
{
    public interface IDeletarCliente
    {
        Task<bool> Execute(Guid id);
    }
}