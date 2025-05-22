namespace Umio.API.Application.Contratos.Servicos
{
    public interface ITokenService
    {
        public string GerarToken(Guid clienteId);
    }
}
