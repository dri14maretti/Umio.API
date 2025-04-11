using Umio.API.Application.Contratos;

namespace Umio.API.Application.CasosDeUso.Enderecos.Interfaces
{
    public interface ICriarEndereco
    {
        public Task<bool> Executar(CriarEnderecoRequest request);
    }
}
