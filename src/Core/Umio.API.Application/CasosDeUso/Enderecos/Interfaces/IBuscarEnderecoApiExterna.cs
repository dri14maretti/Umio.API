using Umio.API.Entities.Entidades;

namespace Umio.API.Application.CasosDeUso.Enderecos.Interfaces
{
    public interface IBuscarEnderecoApiExterna
    {
        public Task<Endereco> Executar(string cep);
    }
}
