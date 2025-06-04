namespace Umio.API.Application.CasosDeUso.Usuarios.Interfaces
{
    public interface ILoginUsuario
    {
        Task<string> GerarToken(string email, string senha);
    }
}
