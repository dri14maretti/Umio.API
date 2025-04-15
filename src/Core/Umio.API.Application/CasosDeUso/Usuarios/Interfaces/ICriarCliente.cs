
namespace Umio.API.Application.CasosDeUso.Usuarios.Interfaces
{
    public interface ICriarCliente
    {
        public Task<bool> Executar(string nome, string email, string telefone);
    }
}
