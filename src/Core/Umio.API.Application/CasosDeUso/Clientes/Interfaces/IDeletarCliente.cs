namespace Umio.API.Application.CasosDeUso.Clientes.Interfaces
{
    public interface IDeletarCliente
    {
        Task<bool> Executar(Guid id);
    }
}