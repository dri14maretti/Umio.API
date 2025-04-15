namespace Umio.API.Application.CasosDeUso.Enderecos.Interfaces
{
    public interface IExcluirEndereco
    {
        public Task<bool> Executar(Guid id);
    }
}
